using Set_Backend.Models;

namespace Set_Backend.DTO;

public class GameDTO
{
    public int Id { get; set; }
    
    public int PlayerId { get; set; }
    
    public int DeckId { get; set; }
    
    public int Hints { get; set; }
    
    public int Fails  { get; set; }
    
    public GameStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? FinishedAt { get; set; }
    
    public DeckDTO Deck { get; set; }
    
    public PlayerDTO Player { get; set; }
    
    public List<CardDTO> TableCards { get; set; }
    
    public List<FoundSet> FoundSets { get; set; }
    
}