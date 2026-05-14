using Set_Backend.Models;
using Set_Backend.DTO;

namespace Set_Backend.Services;

public interface IGameService
{
    Task<IEnumerable<GameDTO>> GetAllGamesAsync();
    Task<GameDTO?> GetGameByIdAsync(int id);
    Task<GameDTO> CreateGameAsync(int id);
    Task<bool> DeleteGameAsync(int id);
    Task<bool> IsGameOverAsync(int id); 
    Task<GameDTO> ProcessFoundSetAsync(int id, List<int> cardIds);
}