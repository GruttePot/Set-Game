using Set_Backend.Models;
using Set_Backend.DTO;

namespace Set_Backend.Services;

public interface ISetService
{
    bool IsValidSet(CardDTO card1, CardDTO card2, CardDTO card3);
    List<Card[]> FindAllSets(List<Card> tableCards);
    Task<bool> ValidateSetAsync(int id, List<int> cardIds);
    Task<int> FindAvailableSetsAsync(int id);
    Task<List<CardDTO>> GetHintAsync(int id);
}