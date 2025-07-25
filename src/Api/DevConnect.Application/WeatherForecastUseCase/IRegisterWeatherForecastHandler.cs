using DevConnect.Shared.DTOs.WeatherForecastUseCase;

namespace DevConnect.Application.WeatherForecastUseCase;

public interface IRegisterWeatherForecastHandler
{
    Task<WeatherForecastDto> HandleAsync(RegisterWeatherForecastCommand command, CancellationToken cancellationToken = default);
}
