using Set_Backend.Models;
using Set_Backend.DTO;

namespace Set_Backend.Services;

public interface IGameService
{
    Task<IEnumerable<GameDTO>> GetGamesAsync();
    Task<GameDTO?> GetGameByIdAsync(int id);
    Task<GameDTO> CreateGameAsync(int id);
    Task<bool> DeleteGameAsync(int id);
    
    Task<Deck> ShuffleDeckAsync(Deck deck);
    Task<Card> DealCardAsync(Deck deck);
    
    Task<bool> ValidateSetAsync(List<Card> cards);
    Task<List<Card>> FindAvailableSetsAsync(Deck deck);
    Task<List<CardDTO>> GetHintAsync(Deck deck);
    Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards);
    
    bool IsValidSet(Card card1, Card card2, Card card3);

    List<Card[]> FindAllSets(List<Card> tableCards);

    List<Card> GenerateGameCards();
}