using Microsoft.EntityFrameworkCore;
using Set_Backend.Models;

namespace Set_Backend.Data;

public class SetGameDbContext : DbContext
{
    public SetGameDbContext(DbContextOptions<SetGameDbContext> options) : base(options) { }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
}