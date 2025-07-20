using DevConnect.Domain.WeatherForecastUseCase;
using DevConnect.Shared.DTOs.WeatherForecastUseCase;

namespace DevConnect.Application.WeatherForecastUseCase;


public sealed class RegisterWeatherForecastHandler : IRegisterWeatherForecastHandler
{
    private readonly IWeatherForecastRepository _repository;

    public RegisterWeatherForecastHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<WeatherForecastDto> HandleAsync(RegisterWeatherForecastCommand command, CancellationToken cancellationToken = default)
    {
        var forecast = WeatherForecast.Register(
            WeatherForecastId.NewId(),
            Temperature.FromCelsius(command.Temperature),
            command.Location,
            command.Description,
            command.Date
        );
        await _repository.AddAsync(forecast, cancellationToken);
        return new WeatherForecastDto(
            forecast.Id.Value,
            forecast.Date,
            forecast.Location,
            forecast.Temperature.Value,
            forecast.Description
        );
    }
}
