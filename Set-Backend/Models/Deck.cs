namespace Set_Backend.Models;

public class Deck
{
    public int Id { get; set; }
    
    public List<Card> Cards { get; set; } = new List<Card>();

}