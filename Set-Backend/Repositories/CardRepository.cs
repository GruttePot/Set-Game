using Set_Backend.Data;
using Set_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Set_Backend.Repositories;

public class CardRepository : ICardRepository
{
    private readonly SetGameDbContext _context;

    public CardRepository(SetGameDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Card>> GetCardsAsync()
    {
        return await _context.Cards.ToListAsync();
    }
    
    public async Task<Card?> GetCardByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
    
    public async Task<Card> CreateCardAsync(Card card)
    {
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<Card> UpdateCardAsync(Card card)
    {
        _context.Entry(card).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return card;
    }
    
    public async Task DeleteCardAsync(Card card)
    {
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }
}