using Microsoft.EntityFrameworkCore;
using Set_Backend.Models;

namespace Set_Backend.Data;

public class SetGameDbContext : DbContext
{
    public SetGameDbContext(DbContextOptions<SetGameDbContext> options) : base(options) { }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<FoundSet > FoundSets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        modelBuilder.Entity<Game>()
            .Property(g => g.CreatedAt)
            .IsRequired();
        
        modelBuilder.Entity<FoundSet>()
            .HasOne(fs => fs.Game)
            .WithMany(g => g.FoundSets)
            .HasForeignKey(fs => fs.GameId);

        modelBuilder.Entity<FoundSet>()
            .HasOne(fs => fs.Game)
            .WithMany(g => g.FoundSets)
            .HasForeignKey(fs => fs.GameId);

        modelBuilder.Entity<FoundSet>()
            .HasOne(f => f.Card1)
            .WithMany()
            .HasForeignKey(f => f.Card1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FoundSet>()
            .HasOne(f => f.Card2)
            .WithMany()
            .HasForeignKey(f => f.Card2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FoundSet>()
            .HasOne(f => f.Card3)
            .WithMany()
            .HasForeignKey(f => f.Card3Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        var cards = GenerateAllCards();
        modelBuilder.Entity<Card>().HasData(cards);
    }
    private static List<Card> GenerateAllCards()
    {
        var cards = new List<Card>();
        var id = 1;
    
        foreach (var colour in Enum.GetValues<CardColour>())
        {
            foreach (var shape in Enum.GetValues<CardShape>())
            {
                foreach (var filling in Enum.GetValues<CardFilling>())
                {
                    foreach (var number in Enum.GetValues<CardNumber>())
                    {
                        cards.Add(new Card
                        {
                            Id = id++,
                            Colour = colour,
                            Shape = shape,
                            Filling = filling,
                            Number = number
                        });
                    }
                }
            }
        }
    
        return cards;
    }
}