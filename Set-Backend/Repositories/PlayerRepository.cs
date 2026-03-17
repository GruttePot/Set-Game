using Set_Backend.Data;
using Set_Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Set_Backend.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly SetGameDbContext _context;
    
    public PlayerRepository(SetGameDbContext context)
    {
        _context = context;
    }
    
    public async Task<Player?> GetPlayerByIdAsync(int id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<Player?> GetPlayerByNameAsync(string name)
    {
        return await _context.Players.FirstOrDefaultAsync(p => p.UserName == name);
    }
    
    public async Task<Player> CreatePlayerAsync(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
    
}