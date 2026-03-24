using Set_Backend.DTO;
using Set_Backend.Repositories;
using Set_Backend.Models;
using AutoMapper;

namespace Set_Backend.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IDeckService _deckService;
    private readonly IMapper _mapper;
    
    public GameService(IGameRepository gameRepository, IMapper mapper, IDeckService deckService)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _deckService = deckService;
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
        
        var deck = new Deck { Cards = gameCards };

        var shuffleDeck = await _deckService.ShuffleDeckAsync(deck);
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
}