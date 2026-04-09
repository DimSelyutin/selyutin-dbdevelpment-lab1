using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class Hall
{
    public int Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Curator { get; set; } = string.Empty;

    public ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
}
