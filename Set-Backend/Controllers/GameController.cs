using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Set_Backend.Models;
using Set_Backend.Services;

namespace Set_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    private int ParsePlayerId()
    {
        var playerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(playerId, out var playerIdInt) ? playerIdInt : 1;
    }
    
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var games = await _gameService.GetGamesAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Game>> GetGame(int id)
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
    public async Task<ActionResult<Game>> CreateGame(Game game)
    {
        var playerId = ParsePlayerId();
        if (playerId == null)
        {
            return BadRequest();
        }
        
        var create_game = await _gameService.CreateGameAsync(game);
        
        return CreatedAtAction(nameof(GetGame), new { id = create_game.Id }, create_game);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Game>> UpdateGame(int id, Game game)
    {
        if (id != game.Id)
        {
            return BadRequest();
        }
        
        var updated_game = await _gameService.UpdateGameAsync(game);
        return Ok(updated_game);
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
        
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        
        await _gameService.DeleteGameAsync(game);
        return NoContent();
    }
    
    // [HttpPost("{id}/check-set")]
    // [Authorize]
    // public async Task<ActionResult<object>> CheckSet(int id, [FromBody] List<int> cardIds)
    // {
    //     var PlayerId = ParsePlayerId();
    //     if (PlayerId == null)
    //     {
    //         return BadRequest();
    //     }
    //     
    //     var checkSet = await _gameService.ValidateSetAsync(List<Card> cards);
    //     
    //     if (checkSet == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     return checkSet;
    // }
    //
    // [HttpGet("{id}/available-sets")]
    // [Authorize]
    // public async Task<ActionResult<int>> GetAvailableSets(int id)
    // {
    //     var playerId = ParsePlayerId();
    //     if (playerId == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     var game = await _gameService.GetGameByIdAsync(id);
    //     if (game == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     var availableSets = await _gameService.FindAvailableSetsAsync(game.Deck);
    //
    //     if (availableSets == null)
    //     {
    //         return BadRequest();
    //     }
    //     return availableSets.Count;
    // }
    //
    //
    // [HttpPost("{id}/hint")]
    // [Authorize]
    // public async Task<ActionResult<List<Card>>> GetHint(int id)
    // {
    //     var playerId = ParsePlayerId();
    //     if (playerId == null)
    //     {
    //         return BadRequest();
    //     }
    //     
    //     var hint = await _gameService.GetHintAsync(deck);
    //
    //     if (hint == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     return hint;
    // }
    //
    // [HttpPost("{id}/draw-card")]
    // [Authorize]
    // public async Task<ActionResult<Card>> DrawCard(int id)
    // {
    //     
    // }
}