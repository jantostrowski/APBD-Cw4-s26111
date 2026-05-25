using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationCF.Models;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    public int Id { get; set; }

    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;

    [MaxLength(300)]
    public string FullName { get; set; } = string.Empty;

    [Column(TypeName = "date")]
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
