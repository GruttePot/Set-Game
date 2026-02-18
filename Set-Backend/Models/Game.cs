namespace Set_Backend.Models;

public class Game
{
    public required int Id { get; set; }
    
    public required int DeckId { get; set; }
    
    public string? Hint { get; set; }
    
    public required string Status { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Ended { get; set; }

    public Deck Deck { get; set; } = null!;

    public List<Player> Players { get; set; } = new List<Player>();

    public List<Card> TableCards { get; set; } = new List<Card>();
}