




using DevConnect.Domain.WeatherForecast;

namespace DevConnect.Application.WeatherForecast;

public sealed class FindWeatherForecastsHandler : IFindWeatherForecastsHandler
{
    private readonly IWeatherForecastRepository _repository;

    public FindWeatherForecastsHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Domain.WeatherForecast.WeatherForecast>> HandleAsync(FindWeatherForecastsQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByDateAndLocationAsync(query.Date, query.Location, cancellationToken);
    }
}
