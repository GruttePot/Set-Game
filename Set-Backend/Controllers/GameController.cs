using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Set_Backend.DTO;
using Set_Backend.Services;

namespace Set_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ISetService _setService;

    private int ParsePlayerId()
    {
        var playerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(playerId, out var playerIdInt) ? playerIdInt : 1;
    }
    
    public GameController(IGameService gameService, ISetService setService)
    {
        _gameService = gameService;
        _setService = setService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames()
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var games = await _gameService.GetAllGamesAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GameDTO>> GetGame(int id)
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return Ok(game);
    }

    [HttpPost("new")]
    [Authorize]
    public async Task<ActionResult<GameDTO>> CreateGame()
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var create_game = await _gameService.CreateGameAsync(playerId);
        
        return CreatedAtAction(nameof(GetGame), new { id = create_game.Id }, create_game);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        await _gameService.DeleteGameAsync(id);
        
        return NoContent();
    }
    
    [HttpPost("{id}/check-set")]
    [Authorize]
    public async Task<ActionResult<object>> CheckSet(int id, [FromBody] List<int> cardIds)
    {
        var PlayerId = ParsePlayerId();
        if (PlayerId == null)
        {
            return BadRequest();
        }
        
        var checkSet = await _setService.ValidateSetAsync(id , cardIds);
        
        if (!checkSet)
        {
            return BadRequest();
        }
    
        return checkSet;
    }
    
    [HttpGet("{id}/available-sets")]
    public async Task<ActionResult<int>> GetAvailableSets(int id)
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var availableSets = await _setService.FindAvailableSetsAsync(id);
    
        if (availableSets == null)
        {
            return NotFound();
        }

        return availableSets;
    }
    
    
    [HttpPost("{id}/hint")]
    public async Task<ActionResult<List<CardDTO>>> GetHint(int id)
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var hint = await _setService.GetHintAsync(id);
    
        if (hint == null)
        {
            return BadRequest();
        }
    
        return hint;
    }
}