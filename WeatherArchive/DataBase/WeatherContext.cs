using Microsoft.EntityFrameworkCore;
using WeatherArchive.Models;

namespace WeatherArchive.DataBase;

public class WeatherContext: DbContext
{
    public WeatherContext(DbContextOptions<WeatherContext> options):base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<WeatherRow> WeatherRows { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherRow>()
            .HasIndex(p => new {p.Date , p.Time}).IsUnique();
    }
}