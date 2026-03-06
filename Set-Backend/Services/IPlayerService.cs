using Set_Backend.Models;

namespace Set_Backend.Services;

public interface IPlayerService
{
    Task<Player?> ValidatePlayer(string name, string passwordHash);
    string GenerateJwtToken(Player player);
}