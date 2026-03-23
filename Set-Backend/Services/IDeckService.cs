using Set_Backend.Models;


namespace Set_Backend.Services;

public interface IDeckService
{
    Task<Deck> ShuffleDeckAsync(Deck deck);
    Task<Card> DealCardAsync(Deck deck);
    Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards);
}