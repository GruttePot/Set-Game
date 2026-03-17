using Microsoft.AspNetCore.Identity;

namespace Set_Backend.Models;

public class Player : IdentityUser
{
    public required int Id { get; set; }
    
    public List<Game> Games { get; set; } = new List<Game>();

}