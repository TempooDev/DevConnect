using DevConnect.Domain.WeatherForecast;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.WeatherForecast;

public sealed class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly WeatherForecastDbContext _dbContext;

    public WeatherForecastRepository(WeatherForecastDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Domain.WeatherForecast.WeatherForecast forecast, CancellationToken cancellationToken = default)
    {
        await _dbContext.WeatherForecasts.AddAsync(forecast, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.WeatherForecast.WeatherForecast>> FindByDateAndLocationAsync(DateTime date, string location, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WeatherForecasts
            .Where(f => f.Date.Date == date.Date && f.Location == location)
            .ToListAsync(cancellationToken);
    }
}
