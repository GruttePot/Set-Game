namespace Set_Backend.Models;

public class FoundSet
{
    public int Id  { get; set; }
    public int GameId { get; set; }
    public DateTime FoundAt  { get; set; }
    
    public int Card1Id  { get; set; }
    public int Card2Id  { get; set; }
    public int Card3Id  { get; set; }
    
    public Card Card1 { get; set; } = null!;
    public Card Card2 { get; set; } = null!;
    public Card Card3 { get; set; } = null!;
    
    public Game Game { get; set; } = null!;

}