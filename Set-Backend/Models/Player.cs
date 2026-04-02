namespace Set_Backend.Models;

public class Player
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public ICollection<Game>? Game { get; set; }
    
}