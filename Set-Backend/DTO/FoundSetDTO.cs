namespace Set_Backend.DTO;

public class FoundSetDTO
{
    public int Id  { get; set; }
    public int GameId { get; set; }
    public DateTime FoundAt  { get; set; }
    
    public CardDTO Card1 { get; set; }
    public CardDTO Card2 { get; set; }
    public CardDTO Card3 { get; set; }
}