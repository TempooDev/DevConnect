using DevConnect.Domain.WeatherForecastUseCase;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.WeatherForecastUseCase;

public sealed class WeatherForecastDbContext : DbContext
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasConversion(
                      weatherForecastId => weatherForecastId.Value,
                      value => new WeatherForecastId(value))
                  .ValueGeneratedOnAdd(); 

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
