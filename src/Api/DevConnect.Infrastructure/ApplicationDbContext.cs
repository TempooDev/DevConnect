using DevConnect.Domain.WeatherForecastUseCase;
using Microsoft.EntityFrameworkCore;
using DevConnect.Domain.AuthUseCase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DevConnect.Shared.Enums;

namespace DevConnect.Infrastructure;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<Role> DevConnectRoles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .HasConversion(
                      roleId => roleId.Value,
                      value => new RoleId(value))
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.Type)
                  .HasConversion(
                      userRole => (int)userRole,
                      value => (UserRole)value)
                  .IsRequired();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();

            entity.HasIndex(e => e.Type).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(e => e.Role)
                  .WithMany()
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
