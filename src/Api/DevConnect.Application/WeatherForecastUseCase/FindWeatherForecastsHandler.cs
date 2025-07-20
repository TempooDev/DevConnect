using DevConnect.Domain.WeatherForecastUseCase;
using DevConnect.Shared.DTOs.WeatherForecastUseCase;

namespace DevConnect.Application.WeatherForecastUseCase;

public sealed class FindWeatherForecastsHandler : IFindWeatherForecastsHandler
{
    private readonly IWeatherForecastRepository _repository;

    public FindWeatherForecastsHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeatherForecastDto>> HandleAsync(FindWeatherForecastsQuery query, CancellationToken cancellationToken = default)
    {
        var forecasts = await _repository.FindByDateAndLocationAsync(query.Date, query.Location, cancellationToken);
        return forecasts.Select(f => new WeatherForecastDto(
            f.Id.Value,
            f.Date,
            f.Location,
            f.Temperature.Value,
            f.Description
        ));
    }
}
