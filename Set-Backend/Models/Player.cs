namespace Set_Backend.Models;

public class Player
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public int Score { get; set; }
    
    public List<Game> Games { get; set; }
 
   // public required int GameId { get; set; }
}