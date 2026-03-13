using Microsoft.EntityFrameworkCore;
using Set_Backend.Models;

namespace Set_Backend.Data;

public class SetGameDbContext : DbContext
{
    public SetGameDbContext(DbContextOptions<SetGameDbContext> options) : base(options) { }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        modelBuilder.Entity<Game>()
            .Property(g => g.Created)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}