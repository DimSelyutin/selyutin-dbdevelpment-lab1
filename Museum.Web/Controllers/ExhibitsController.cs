using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum.Web.Data;

namespace Museum.Web.Controllers;

public class ExhibitsController(MuseumContext context) : Controller
{
    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> Index()
    {
        var exhibits = await context.Exhibits
            .Include(x => x.Hall)
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
        return View(exhibits);
    }

    [ResponseCache(CacheProfileName = "MuseumCache")]
    public async Task<IActionResult> UnderRestoration()
    {
        var exhibits = await context.Exhibits
            .Include(x => x.Hall)
            .AsNoTracking()
            .Where(x => x.IsUnderRestoration)
            .OrderBy(x => x.Name)
            .ToListAsync();
        return View(exhibits);
    }
}
