namespace Set_Backend.Models;

public enum CardColour { Red, Green, Purple}
public enum CardShape { Diamond, Squiggle, Oval }
public enum CardFilling { Solid, Striped, Open }


public class Card
{
    public required int Id { get; set; }
    
    public string Colour { get; set; }
    
    public string Shape { get; set; }
    
    public string Filling { get; set; }
    
    public string Number { get; set; }
    
}