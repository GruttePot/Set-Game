using Set_Backend.Models;


namespace Set_Backend.Services;

public interface IDeckService
{
    Task<List<Card>> ShuffleDeckAsync(List<Card> cards);
    Task<Card> DealCardAsync(List<Card> cards);
}