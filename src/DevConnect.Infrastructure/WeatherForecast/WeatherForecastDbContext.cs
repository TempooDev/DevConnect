using DevConnect.Domain.WeatherForecast;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.WeatherForecast;

public sealed class WeatherForecastDbContext : DbContext
{
    public DbSet<Domain.WeatherForecast.WeatherForecast> WeatherForecasts { get; set; }

    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.WeatherForecast.WeatherForecast>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Location).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.OwnsOne(e => e.Temperature, t =>
            {
                t.Property(p => p.Value).HasColumnName("Temperature");
            });
        });
    }
}
