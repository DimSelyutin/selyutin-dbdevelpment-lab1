using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class Guide
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Languages { get; set; } = string.Empty;

    public ICollection<TourSchedule> TourSchedules { get; set; } = new List<TourSchedule>();
}
