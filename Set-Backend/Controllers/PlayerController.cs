using Microsoft.AspNetCore.Mvc;
using Set_Backend.DTO;
using Set_Backend.Services;


namespace Set_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    
    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

     [HttpPost("login")]
     public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
     {
         var playerDto = await _playerService.ValidatePlayer(credentials.UserName, credentials.PasswordHash);
     
         if (playerDto != null)
         {
             var token = _playerService.GenerateJwtToken(playerDto);
             return Ok(new { Token = token, PlayerId = playerDto.Id, PlayerName = playerDto.Name });
         }
     
        return Unauthorized();
     }
}