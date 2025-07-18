




using DevConnect.Domain.WeatherForecast;

namespace DevConnect.Application.WeatherForecast;

public sealed class RegisterWeatherForecastHandler : IRegisterWeatherForecastHandler
{
    private readonly IWeatherForecastRepository _repository;

    public RegisterWeatherForecastHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RegisterWeatherForecastCommand command, CancellationToken cancellationToken = default)
    {
        var forecast = WeatherForecast.Register(
            WeatherForecastId.NewId(),
            Temperature.FromCelsius(command.Temperature),
            command.Location,
            command.Description,
            command.Date
        );
        await _repository.AddAsync(forecast, cancellationToken);
    }
}
