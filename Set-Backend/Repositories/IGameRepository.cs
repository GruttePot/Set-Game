using Set_Backend.Models;

namespace Set_Backend.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> CreateGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(Game game);
}