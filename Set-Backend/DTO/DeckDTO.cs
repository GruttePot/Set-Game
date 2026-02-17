namespace Set_Backend.DTO;

public class DeckDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<CardDTO> Cards { get; set; }
}