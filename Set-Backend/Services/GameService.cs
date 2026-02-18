using Set_Backend.Repositories;
using Set_Backend.Models;

namespace Set_Backend.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await _gameRepository.GetGamesAsync();
    }
    
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _gameRepository.GetGameByIdAsync(id);
    }
    
    public async Task<Game> CreateGameAsync(Game game)
    {
        return await _gameRepository.CreateGameAsync(game);
    }
    
    public async Task<Game> UpdateGameAsync(Game game)
    {
        return await _gameRepository.UpdateGameAsync(game);
    }
    
    public async Task DeleteGameAsync(Game game)
    {
        await _gameRepository.DeleteGameAsync(game);
    }
}