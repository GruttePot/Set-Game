namespace Set_Backend.DTO;

public class PlayerDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<GameDTO>? Game { get; set; }
}