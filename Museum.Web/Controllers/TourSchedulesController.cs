using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum.Web.Data;

namespace Museum.Web.Controllers;

public class TourSchedulesController(MuseumContext context) : Controller
{
    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> Index()
    {
        var items = await context.TourSchedules
            .Include(x => x.Excursion)
            .Include(x => x.Guide)
            .Include(x => x.TourGroup)
            .AsNoTracking()
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Time)
            .ToListAsync();
        return View(items);
    }

    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> CurrentWeek()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var start = today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
        if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
        {
            start = today.AddDays(-6);
        }

        var end = start.AddDays(6);

        var items = await context.TourSchedules
            .Include(x => x.Excursion)
            .Include(x => x.Guide)
            .Include(x => x.TourGroup)
            .AsNoTracking()
            .Where(x => x.Date >= start && x.Date <= end)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Time)
            .ToListAsync();

        ViewData["WeekRange"] = $"{start:dd.MM.yyyy} - {end:dd.MM.yyyy}";
        return View(items);
    }
}
