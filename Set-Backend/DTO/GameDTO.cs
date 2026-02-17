namespace Set_Backend.DTO;

public class GameDTO
{
    public int Id { get; set; }
    
    public int DeckId { get; set; }
    
    public string Hints { get; set; }
    
    public string Status { get; set; }
    
    public DeckDTO Deck { get; set; }
    
    //public List<CardDTO> CardsOnTable { get; set; }
}