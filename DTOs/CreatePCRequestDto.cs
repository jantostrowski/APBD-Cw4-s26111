using System.ComponentModel.DataAnnotations;

namespace WebApplicationCF.DTOs;

public class CreatePCRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public double Weight { get; set; }

    [Range(0, int.MaxValue)]
    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
