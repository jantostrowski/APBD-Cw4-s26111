namespace WebApplicationCF.DTOs;

public class PCDetailsResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }

    public ICollection<PCComponentResponseDto> Components { get; set; } = new List<PCComponentResponseDto>();
}
