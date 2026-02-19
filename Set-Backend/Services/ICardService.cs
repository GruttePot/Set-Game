using Set_Backend.Models;
namespace Set_Backend.Services;

public interface ICardService
{
    Task<IEnumerable<Card>> GetCardsAsync();
    Task<Card?> GetCardByIdAsync(int id);
    Task<Card> CreateCardAsync(Card card);
    Task<Card> UpdateCardAsync(Card card);
    Task DeleteCardAsync(Card card);
}