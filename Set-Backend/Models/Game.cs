namespace Set_Backend.Models;

public class Game
{
    public required int Id { get; set; }
    
    public required int DeckId { get; set; }
    
    public string Hint { get; set; }
    
    public string Status { get; set; }
    
    public Deck Deck { get; set; }
    
    // public DateTime Created { get; set; }
    // public List<Player> Players { get; set; }
    // public List<Card> Cards { get; set; }
}