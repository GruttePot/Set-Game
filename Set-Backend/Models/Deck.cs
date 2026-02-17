namespace Set_Backend.Models;

public class Deck
{
    public required int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Card> Cards { get; set; }
    
}