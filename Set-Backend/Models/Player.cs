namespace Set_Backend.Models;

public class Player
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public int Score { get; set; }
    
    public List<Game> Games { get; set; } = new List<Game>();

}