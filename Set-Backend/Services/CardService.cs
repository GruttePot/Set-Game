using Set_Backend.Repositories;
using Set_Backend.Models;

namespace Set_Backend.Services;

public class CardService : ICardService
{
    public readonly ICardRepository _cardRepository;
    
    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    
    public async Task<IEnumerable<Card>> GetCardsAsync()
    {
        return await _cardRepository.GetCardsAsync();
    }
    
    public async Task<Card?> GetCardByIdAsync(int id)
    {
        return await _cardRepository.GetCardByIdAsync(id);
    }
    
    public async Task<Card> CreateCardAsync(Card card)
    {
        return await _cardRepository.CreateCardAsync(card);
    }
    
    public async Task<Card> UpdateCardAsync(Card card)
    {
        return await _cardRepository.UpdateCardAsync(card);
    }
    
    public async Task DeleteCardAsync(Card card)
    {
        await _cardRepository.DeleteCardAsync(card);
    }
}