namespace DevConnect.Shared.DTOs.WeatherForecastUseCase;

public sealed record RegisterWeatherForecastCommand(decimal Temperature, string Location, string Description, DateTime Date);
