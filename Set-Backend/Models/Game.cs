namespace Set_Backend.Models;

public enum GameStatus
{
    Active,
    Finished,
    Paused
}

public class Game
{
    public int Id { get; set; }

    public required int PlayerId { get; set; }
    
    public int Hints { get; set; }
    
    public int Fails { get; set; }
    
    public required GameStatus Status { get; set; }
    
    public List<FoundSet> FoundSets { get; set; } = new List<FoundSet>();
    
    public List<Card> TableCards { get; set; } =  new List<Card>();
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? FinishedAt { get; set; }
    
    public Player Player { get; set; } = null!;

    public Deck Deck { get; set; } = null!;
}