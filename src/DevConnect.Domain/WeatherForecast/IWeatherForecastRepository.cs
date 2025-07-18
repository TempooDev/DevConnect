namespace DevConnect.Domain.WeatherForecast;

public interface IWeatherForecastRepository
{
    Task AddAsync(WeatherForecast forecast, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WeatherForecast>> FindByDateAndLocationAsync(DateTime date, string location, CancellationToken cancellationToken = default);
}
