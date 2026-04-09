using Microsoft.EntityFrameworkCore;
using Museum.Web.Data;
using Museum.Web.Models;

namespace Museum.Web.Middleware;

public class DatabaseSeedMiddleware(RequestDelegate next)
{
    private static bool _initialized;
    private static readonly SemaphoreSlim SeedLock = new(1, 1);

    public async Task InvokeAsync(HttpContext context, MuseumContext dbContext)
    {
        if (!_initialized)
        {
            await SeedLock.WaitAsync();
            try
            {
                if (!_initialized)
                {
                    await dbContext.Database.MigrateAsync();
                    await SeedIfEmptyAsync(dbContext);
                    _initialized = true;
                }
            }
            finally
            {
                SeedLock.Release();
            }
        }

        await next(context);
    }

    private static async Task SeedIfEmptyAsync(MuseumContext dbContext)
    {
        if (await dbContext.Halls.AnyAsync())
        {
            return;
        }

        var halls = new List<Hall>
        {
            new() { Number = 1, Name = "Древний мир", Curator = "Иванова Т.А." },
            new() { Number = 2, Name = "Средневековье", Curator = "Петров С.Н." },
            new() { Number = 3, Name = "Новое время", Curator = "Сидорова Е.В." }
        };

        var exhibits = new List<Exhibit>
        {
            new() { Name = "Амфора", Description = "Греческая амфора V века до н.э.", Era = "Античность", Material = "Керамика", IsUnderRestoration = false, Hall = halls[0] },
            new() { Name = "Рыцарский меч", Description = "Меч XIV века", Era = "Средневековье", Material = "Сталь", IsUnderRestoration = true, Hall = halls[1] },
            new() { Name = "Механические часы", Description = "Часы XVII века", Era = "Новое время", Material = "Латунь", IsUnderRestoration = false, Hall = halls[2] }
        };

        var excursions = new List<Excursion>
        {
            new() { Title = "Сокровища античности", Topic = "Античность", DurationMinutes = 60, Price = 500 },
            new() { Title = "Эпоха рыцарей", Topic = "Средневековье", DurationMinutes = 75, Price = 650 },
            new() { Title = "Путь технологий", Topic = "Новое время", DurationMinutes = 50, Price = 550 }
        };

        var guides = new List<Guide>
        {
            new() { FullName = "Анна Сергеева", Languages = "Русский, Английский" },
            new() { FullName = "Максим Лебедев", Languages = "Русский, Немецкий" },
            new() { FullName = "Екатерина Орлова", Languages = "Русский, Французский" }
        };

        var groups = new List<TourGroup>
        {
            new() { Name = "Школьная группа 8А", Size = 22 },
            new() { Name = "Студенты исторического факультета", Size = 18 },
            new() { Name = "Туристы из Минска", Size = 12 }
        };

        var today = DateOnly.FromDateTime(DateTime.Today);
        var schedules = new List<TourSchedule>
        {
            new() { Date = today, Time = new TimeOnly(10, 0), Excursion = excursions[0], Guide = guides[0], TourGroup = groups[0] },
            new() { Date = today.AddDays(1), Time = new TimeOnly(12, 30), Excursion = excursions[1], Guide = guides[1], TourGroup = groups[1] },
            new() { Date = today.AddDays(2), Time = new TimeOnly(15, 0), Excursion = excursions[2], Guide = guides[2], TourGroup = groups[2] },
            new() { Date = today.AddDays(3), Time = new TimeOnly(11, 0), Excursion = excursions[1], Guide = guides[0], TourGroup = groups[2] }
        };

        dbContext.AddRange(halls);
        dbContext.AddRange(exhibits);
        dbContext.AddRange(excursions);
        dbContext.AddRange(guides);
        dbContext.AddRange(groups);
        dbContext.AddRange(schedules);

        await dbContext.SaveChangesAsync();
    }
}
