using Set_Backend.DTO;

namespace Set_Backend.Services;

public interface IPlayerService
{
    Task<PlayerDTO?> ValidatePlayer(string name, string passwordHash);
    string GenerateJwtToken(PlayerDTO player);
}