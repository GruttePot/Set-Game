namespace Set_Backend.DTO;

public class LoginRequest
{
    public required string Name { get; set; }
    
    public required string PasswordHash { get; set; }
}