using Set_Backend.Data;
using Set_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Set_Backend.Repositories;

public class GameRepository : IGameRepository
{
    private readonly SetGameDbContext _context;
    
    public GameRepository(SetGameDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await _context.Games.ToListAsync();
    }
    
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Game> CreateGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _context.Entry(game).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return game;
    }
    
    public async Task DeleteGameAsync(Game game)
    {
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task<FoundSet> SaveFoundSetAsync(FoundSet foundSet)
    {
        _context.FoundSets.Add(foundSet);
        await _context.SaveChangesAsync();
        return foundSet;
    }
    
    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<Card?> GetCardByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
}