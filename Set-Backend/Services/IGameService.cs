using Set_Backend.Models;

namespace Set_Backend.Services;

public interface IGameService
{
    Task<IEnumerable<Game>> GetGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> CreateGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(Game game);
    
    Task<Deck> ShuffleDeckAsync(Deck deck);
    Task<Card> DealCardAsync(Deck deck);
    Task<bool> ValidateSetAsync(List<Card> cards);
    Task<List<Card>> FindAvailableSetsAsync(Deck deck);
    Task<List<Card>> GetHintAsync(Deck deck);
    Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards);
    
    bool IsValidSet(Card card1, Card card2, Card card3);

    List<Card[]> FindAllSets(List<Card> tableCards);

}