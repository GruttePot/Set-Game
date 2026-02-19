using Microsoft.AspNetCore.Mvc;
using Set_Backend.Models;
using Set_Backend.Services;

namespace Set_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    
    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Card>>> GetAllCards()
    {
        var cards = await _cardService.GetCardsAsync();
        return Ok(cards);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCard(int id)
    {
        var card = await _cardService.GetCardByIdAsync(id);
        if (card == null)
        {
            return NotFound();
        }
        return Ok(card);
    }

    [HttpPost]
    public async Task<ActionResult<Card>> CreateCard(Card card)
    {
        var create_card = await _cardService.CreateCardAsync(card);
        return CreatedAtAction(nameof(GetCard), new { id = create_card.Id }, create_card);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Card>> UpdateCard(int id, Card card)
    {
        if (id != card.Id)
        {
            return BadRequest();
        }
        var updated_card = await _cardService.UpdateCardAsync(card);
        return Ok(updated_card);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard(Card card)
    {
        await _cardService.DeleteCardAsync(card);
        return Ok();
    }
}