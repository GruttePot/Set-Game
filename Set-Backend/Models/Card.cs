namespace Set_Backend.Models;

public enum CardColour { Red, Green, Purple}
public enum CardShape { Diamond, Squiggle, Oval }
public enum CardFilling { Solid, Striped, Open }


public class Card
{
    public required int Id { get; set; }
    
    public CardColour Colour { get; set; }
    
    public CardShape Shape { get; set; }
    
    public CardFilling Filling { get; set; }
    
    public int Number { get; set; }
    
}