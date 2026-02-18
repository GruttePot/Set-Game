using Microsoft.AspNetCore.Mvc;
using Set_Backend.Models;
using Set_Backend.Services;

namespace Set_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
    {
        var games = await _gameService.GetGamesAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> CreateGame(Game game)
    {
        var create_game = await _gameService.CreateGameAsync(game);
        
        return CreatedAtAction(nameof(GetGame), new { id = create_game.Id }, create_game);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Game game)
    {
        await _gameService.DeleteGameAsync(game);
        return NoContent();
    }
}