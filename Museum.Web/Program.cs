using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Museum.Web.Data;
using Museum.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MuseumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MuseumConnection")));

builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("MuseumCache", new CacheProfile
    {
        Duration = 270
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<DatabaseSeedMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
