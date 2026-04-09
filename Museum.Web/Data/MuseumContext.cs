using Microsoft.EntityFrameworkCore;
using Museum.Web.Models;

namespace Museum.Web.Data;

public class MuseumContext(DbContextOptions<MuseumContext> options) : DbContext(options)
{
    public DbSet<Exhibit> Exhibits => Set<Exhibit>();
    public DbSet<Hall> Halls => Set<Hall>();
    public DbSet<Excursion> Excursions => Set<Excursion>();
    public DbSet<Guide> Guides => Set<Guide>();
    public DbSet<TourGroup> TourGroups => Set<TourGroup>();
    public DbSet<TourSchedule> TourSchedules => Set<TourSchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Excursion>()
            .Property(x => x.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Exhibit>()
            .HasOne(x => x.Hall)
            .WithMany(x => x.Exhibits)
            .HasForeignKey(x => x.HallId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
