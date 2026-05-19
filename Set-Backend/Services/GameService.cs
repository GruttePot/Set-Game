using Set_Backend.DTO;
using Set_Backend.Repositories;
using Set_Backend.Models;
using AutoMapper;

namespace Set_Backend.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IDeckService _deckService;
    private readonly ISetService _setService;
    private readonly IMapper _mapper;
    
    public GameService(IGameRepository gameRepository, IMapper mapper, IDeckService deckService, ISetService setService)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _deckService = deckService;
        _setService = setService;
    }
    
    public async Task<IEnumerable<GameDTO>> GetAllGamesAsync(int id)
    {
        var games = await _gameRepository.GetAllGamesAsync(id);
        
        return _mapper.Map<IEnumerable<GameDTO>>(games);
    }

    public async Task<GameDTO?> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        return _mapper.Map<GameDTO>(game);

    }

    public async Task<GameDTO> CreateGameAsync(int id)
    {
        var gameCards = (await _gameRepository.GetAllCardsAsync()).ToList();
        
        var shuffledDeck = await _deckService.ShuffleDeckAsync(gameCards);

        var tableCards = shuffledDeck.TakeLast(12).ToList();

        var deckCards = shuffledDeck.SkipLast(12).ToList();
        
        var game = new Game
        {
            PlayerId = id,
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active,
            Deck = deckCards,
            Hints = 10,
            Fails = 0,
            AvailableSets = _setService.FindAllSets(tableCards).Count,
            FoundSets = new List<FoundSet>(),
            TableCards = tableCards
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

    public async Task<bool> IsGameOverAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null || game.Status == GameStatus.Finished)
            return true;
        
        var availableSets = await _setService.FindAvailableSetsAsync(id);
        
        if (game.Deck.Count == 0 && availableSets == 0)
        {
            game.Status = GameStatus.Finished;
            game.FinishedAt = DateTime.UtcNow;
            await _gameRepository.UpdateGameAsync(game);
            return true;
        }
        
        return false;
    }
    
    public async Task<GameDTO> ProcessFoundSetAsync(int id, List<int> cardIds)
    {
        var valid = await _setService.ValidateSetAsync(id, cardIds);
        if (!valid)
            throw new InvalidOperationException("Set invalid");

        await _setService.SaveFoundSetAsync(id, cardIds);
        
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null)
            throw new InvalidOperationException("Game not available");
        
        var removeCards = game.TableCards
            .Where(c => cardIds.Contains(c.Id))
            .ToList();

        foreach (var card in removeCards)
        {
            game.TableCards.Remove(card);
        }
        
        // Deck bijvullen met eerste 12 cards
        while (game.TableCards.Count < 12 && game.Deck.Count > 0)
        {
            var newCard = await _deckService.DealCardAsync(game.Deck);
            game.TableCards.Add(newCard);
        }
        // Controlleer als een set bestaat, zo niet blijf een card toevoegen tot er wel 1 is
        while (game.Deck.Count > 0 && _setService.FindAllSets(game.TableCards).Count == 0)
        {
            int cardsToAdd = Math.Min(3, game.Deck.Count);
            for (int i = 0; i < cardsToAdd; i++)
            {
                var newCard = await _deckService.DealCardAsync(game.Deck);
                game.TableCards.Add(newCard);
            }
        }
        
        await _gameRepository.UpdateGameAsync(game);
        
        await IsGameOverAsync(id);
        
        var updatedGame = await _gameRepository.GetGameByIdAsync(id);
        updatedGame.AvailableSets = _setService.FindAllSets(updatedGame.TableCards).Count;
        return _mapper.Map<GameDTO>(updatedGame);
    }
}