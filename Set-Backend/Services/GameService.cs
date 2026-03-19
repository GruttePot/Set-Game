using Set_Backend.DTO;
using Set_Backend.Repositories;
using Set_Backend.Models;
using AutoMapper;

namespace Set_Backend.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly Random _random = new Random();
    
    public GameService(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GameDTO>> GetGamesAsync()
    {
        var games = await _gameRepository.GetGamesAsync();
        
        return _mapper.Map<IEnumerable<GameDTO>>(games);
    }

    public async Task<GameDTO?> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        return _mapper.Map<GameDTO>(game);

    }

    public async Task<GameDTO> CreateGameAsync(int id)
    {
        var gameCards = GenerateGameCards();
        
        var deck = new Deck { Cards = gameCards };

        var shuffleDeck = await ShuffleDeckAsync(deck);
        shuffleDeck.Cards = shuffleDeck.Cards.Take(12).ToList();

        var game = new Game
        {
            PlayerId = id,
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active,
            Deck = shuffleDeck,
            Hints = 0,
            Fails = 0,
            FoundSets = new List<FoundSet>()
        };
            
         var createdGame =  await _gameRepository.CreateGameAsync(game);
         return _mapper.Map<GameDTO>(createdGame);
    }
    
    public async Task<bool> DeleteGameAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        
        if (game == null)
        {
            return false;
        }
        await _gameRepository.DeleteGameAsync(game);
        return true;
    }

    public async Task<Deck> ShuffleDeckAsync(Deck deck)
    {
        deck.Cards = deck.Cards.OrderBy(_ => _random.Next()).ToList();
        return await Task.FromResult(deck);
    }

    public async Task<Card> DealCardAsync(Deck deck)
    {
        if (deck.Cards.Count == 0)
        {
            throw new InvalidOperationException("No Card left");
        }
        
        var card = deck.Cards[0];
        deck.Cards.RemoveAt(0);
        return await Task.FromResult(card);
    }
    
    public async Task<bool> ValidateSetAsync(List<Card> cards)
    {
        if (cards.Count != 3)
        {
            return await Task.FromResult(false);
        }
        
        return await Task.FromResult(IsValidSet(cards[0], cards[1], cards[2]));
    }

    public bool IsValidSet(Card card1, Card card2, Card card3)
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

    public Task<List<Card>> FindAvailableSetsAsync(Deck deck)
    {
        var sets = FindAllSets(deck.Cards);
        
        if (sets.Count > 0)
        {
            return Task.FromResult(sets.SelectMany(set => set).ToList());
        }
        
        return Task.FromResult(new List<Card>());
    }
    
    public async Task<List<CardDTO>> GetHintAsync(Deck deck)
    {
        var sets = FindAllSets(deck.Cards);
        
        if (sets.Count > 0)
        {
            var randomSet = sets[_random.Next(sets.Count)];
            var cardDTOs = randomSet.Select(card => _mapper.Map<CardDTO>(card)).ToList();
            
            return await Task.FromResult(cardDTOs);
        }
        
        return await Task.FromResult(new List<CardDTO>());
    }

    public async Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards)
    {
        if (!IsValidSet(cards[0], cards[1], cards[2]))
        {
            return await DealCardAsync(deck);
        }
        
        throw new InvalidOperationException("Valid set, cannot draw card");
    }

    public List<Card[]> FindAllSets(List<Card> tableCards)
    {
        var validSets = new List<Card[]>();
        var cardCount = tableCards.Count;

        for (int i = 0; i < cardCount - 2; i++)
        {
            for (int j = i + 1; j < cardCount - 1; j++)
            {
                for (int k = j + 1; k < cardCount; k++)
                {
                    if (IsValidSet(tableCards[i], tableCards[j], tableCards[k]))
                    {
                        validSets.Add(new[] { tableCards[i], tableCards[j], tableCards[k] });
                    }
                }
            }
        }
        return validSets;
    }

    public List<Card> GenerateGameCards()
    {
        var cards = new List<Card>();
        var id = 1;
        
        foreach (var colour in Enum.GetValues<CardColour>())
        {
            foreach (var shape in Enum.GetValues<CardShape>())
            {
                foreach (var filling in Enum.GetValues<CardFilling>())
                {
                    foreach (var number in Enum.GetValues<CardNumber>())
                    {
                        cards.Add(new Card
                        {
                            Colour = colour,
                            Shape = shape,
                            Filling = filling,
                            Number = number
                        });
                    }
                }
            }
        }
    
        return cards;
    }
}