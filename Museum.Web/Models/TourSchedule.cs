using System.ComponentModel.DataAnnotations;

namespace Museum.Web.Models;

public class TourSchedule
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public int ExcursionId { get; set; }
    public Excursion? Excursion { get; set; }

    public int GuideId { get; set; }
    public Guide? Guide { get; set; }

    public int TourGroupId { get; set; }
    public TourGroup? TourGroup { get; set; }
}
