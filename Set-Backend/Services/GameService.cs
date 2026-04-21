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
    
    public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllGamesAsync();
        
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
        
        var shuffledDeck = await _deckService.ShuffleDeckAsync(gameCards);

        var tableCards = shuffledDeck.TakeLast(12).ToList();

        var deckCards = shuffledDeck.SkipLast(12).ToList();
        
        var game = new Game
        {
            PlayerId = id,
            CreatedAt = DateTime.UtcNow,
            Status = GameStatus.Active,
            Deck = deckCards,
            Hints = 0,
            Fails = 0,
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
                            Id = id++,
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

        while (game.TableCards.Count < 12 && game.Deck.Count > 0)
        {
            var setsAvailable = _setService.FindAllSets(game.TableCards);

            if (setsAvailable.Count > 0)
                break;

            try
            {
                var newCard = await _deckService.DrawCardIfNotSetAsync(game.Deck, game.TableCards);
                game.TableCards.Add(newCard);
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
        await _gameRepository.UpdateGameAsync(game);
        return _mapper.Map<GameDTO>(game);
    }
}