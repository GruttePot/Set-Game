using Set_Backend.Models;

namespace Set_Backend.Repositories;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByIdAsync(int id);
    Task<Player?> GetPlayerByNameAsync(string name);
    Task<Player> CreatePlayerAsync(Player player);
}