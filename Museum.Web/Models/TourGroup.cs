using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class TourGroup
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Size { get; set; }

    public ICollection<TourSchedule> TourSchedules { get; set; } = new List<TourSchedule>();
}
