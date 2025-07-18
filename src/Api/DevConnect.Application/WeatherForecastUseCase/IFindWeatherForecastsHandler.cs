namespace DevConnect.Application.WeatherForecastUseCase;

public interface IFindWeatherForecastsHandler
{
    Task<IEnumerable<WeatherForecastDto>> HandleAsync(FindWeatherForecastsQuery query, CancellationToken cancellationToken = default);
}
