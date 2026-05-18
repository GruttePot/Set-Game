using Set_Backend.Models;
using Set_Backend.DTO;
using Set_Backend.Repositories;
using AutoMapper;

namespace Set_Backend.Services;

public class SetService : ISetService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly Random _random = new Random();

    public SetService(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }
    
    public bool IsValidSet(CardDTO card1, CardDTO card2, CardDTO card3)
    {
        return IsAttributeValid(card1.Colour, card2.Colour, card3.Colour) &&
               IsAttributeValid(card1.Shape, card2.Shape, card3.Shape) &&
               IsAttributeValid(card1.Filling, card2.Filling, card3.Filling) &&
               IsAttributeValid(card1.Number, card2.Number, card3.Number);
    }

    private bool IsAttributeValid<T>(T attr1, T attr2, T attr3)
    {
        var attributes = new[] { attr1, attr2, attr3 };
        var distinctCount = attributes.Distinct().Count();
        
        return distinctCount == 1 || distinctCount == 3;
    }
    
   public List<Card[]> FindAllSets(List<Card> tableCards)
{
    var validSets = new List<Card[]>();
    var cardCount = tableCards.Count;
    
    Action<List<Card>, int> backtrack = null;
    backtrack = (currentSet, startIndex) =>
    {
        
        if (currentSet.Count == 3)
        {
            var card1DTO = _mapper.Map<CardDTO>(currentSet[0]);
            var card2DTO = _mapper.Map<CardDTO>(currentSet[1]);
            var card3DTO = _mapper.Map<CardDTO>(currentSet[2]);
            
            if (IsValidSet(card1DTO, card2DTO, card3DTO))
            {
                validSets.Add(new[] { currentSet[0], currentSet[1], currentSet[2] });
            }
            return;
        }
        
   
        for (int i = startIndex; i < cardCount; i++)
        {
         
            if (currentSet.Count == 2)
            {
                var card1DTO = _mapper.Map<CardDTO>(currentSet[0]);
                var card2DTO = _mapper.Map<CardDTO>(currentSet[1]);
                var card3DTO = _mapper.Map<CardDTO>(tableCards[i]);
                
                // Skip deze kaart als het geen geldige set kan vormen
                if (!IsValidSet(card1DTO, card2DTO, card3DTO))
                {
                    continue;
                }
            }
         
            currentSet.Add(tableCards[i]);
            
            backtrack(currentSet, i + 1);
            
            currentSet.RemoveAt(currentSet.Count - 1);
        }
    };
    backtrack(new List<Card>(), 0);
    
    return validSets;
}
    
    public async Task<bool> ValidateSetAsync(int id, List<int> cards)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null)
        {
            return false;
        }
        
        var selectedCards = game.TableCards.Where(c => cards.Contains(c.Id)).ToList();

        if (selectedCards.Count != 3)
        {
            return await Task.FromResult(false);
        }
        
        var card1DTO = _mapper.Map<CardDTO>(selectedCards[0]);
        var card2DTO = _mapper.Map<CardDTO>(selectedCards[1]);
        var card3DTO = _mapper.Map<CardDTO>(selectedCards[2]);
        
        return await Task.FromResult(IsValidSet(card1DTO, card2DTO, card3DTO));
    }
    
    public async Task<int> FindAvailableSetsAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null)
        {
            return 0;
        }
        var sets = FindAllSets(game.TableCards);
        
        return await Task.FromResult(sets.Count);
    }
    
    public async Task<List<CardDTO>> GetHintAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null || game.Hints <= 0)
        {
            return new List<CardDTO>();
        }
       
        game.Hints--;
        
        await _gameRepository.UpdateGameAsync(game);
        
        var sets = FindAllSets(game.TableCards);
        
        if (sets.Count > 0)
        {
            var randomSet = sets[_random.Next(sets.Count)];
            var cardDTOs = randomSet.Take(2).Select(card => _mapper.Map<CardDTO>(card)).ToList();
            
            return await Task.FromResult(cardDTOs);
        }
        
        return await Task.FromResult(new List<CardDTO>());
    }

    public async Task SaveFoundSetAsync(int id, List<int> cardIds)
    {
        var validSet = await ValidateSetAsync(id, cardIds);
        if (!validSet)
        {
            throw new InvalidOperationException("Invalid set");
        }

        var foundSet = new FoundSet
        {
            GameId = id,
            Card1Id = cardIds[0],
            Card2Id = cardIds[1],
            Card3Id = cardIds[2],
            FoundAt = DateTime.UtcNow
        };
        
        await _gameRepository.SaveFoundSetAsync(foundSet);
    }
    
}