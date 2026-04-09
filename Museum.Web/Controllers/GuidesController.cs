using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum.Web.Data;
using Museum.Web.ViewModels;

namespace Museum.Web.Controllers;

public class GuidesController(MuseumContext context) : Controller
{
    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> Index()
    {
        var guides = await context.Guides
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .ToListAsync();
        return View(guides);
    }

    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> Workload()
    {
        var workload = await context.Guides
            .AsNoTracking()
            .Select(g => new GuideWorkloadViewModel
            {
                GuideName = g.FullName,
                Languages = g.Languages,
                ExcursionsCount = g.TourSchedules.Count
            })
            .OrderByDescending(x => x.ExcursionsCount)
            .ThenBy(x => x.GuideName)
            .ToListAsync();

        return View(workload);
    }
}
