using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class Excursion
{
    public int Id { get; set; }

    [Required]
    [StringLength(160)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Topic { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }

    public ICollection<TourSchedule> TourSchedules { get; set; } = new List<TourSchedule>();
}
