using Set_Backend.Models;

namespace Set_Backend.DTO;

public class CardDTO
{
    public int Id { get; set; }
    
    public CardColour Colour { get; set; }
    
    public CardShape Shape { get; set; }
    
    public CardFilling Filling { get; set; }
    
    public CardNumber Number { get; set; }
}