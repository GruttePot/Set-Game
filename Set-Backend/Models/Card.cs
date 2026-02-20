namespace Set_Backend.Models;

public enum CardColour { Red, Green, Purple}
public enum CardShape { Diamond, Squiggle, Oval }
public enum CardFilling { Solid, Striped, Open }
public enum CardNumber { One = 1, Two = 2, Three = 3 }

public class Card
{
    public required int Id { get; set; }
    
    public CardColour Colour { get; set; }
    
    public CardShape Shape { get; set; }
    
    public CardFilling Filling { get; set; }
    
    public CardNumber Number { get; set; }
    
}