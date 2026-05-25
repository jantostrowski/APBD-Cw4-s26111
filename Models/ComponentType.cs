using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationCF.Models;

[Table("ComponentTypes")]
public class ComponentType
{
    public int Id { get; set; }

    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
