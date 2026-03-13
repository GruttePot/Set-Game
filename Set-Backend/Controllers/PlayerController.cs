using Microsoft.AspNetCore.Mvc;
using Set_Backend.Models;
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
         var player = await _playerService.ValidatePlayer(credentials.Name, credentials.PasswordHash);
     
         if (player != null)
         {
             var token = _playerService.GenerateJwtToken(player);
             return Ok(new { Token = token, PlayerId = player.Id, PlayerName = player.Name });
         }
     
        return Unauthorized();
     }
}