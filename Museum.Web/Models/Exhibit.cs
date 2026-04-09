using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class Exhibit
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(800)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Era { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Material { get; set; } = string.Empty;

    public bool IsUnderRestoration { get; set; }

    public int? HallId { get; set; }
    public Hall? Hall { get; set; }
}
