namespace DevConnect.Application.WeatherForecastUseCase;

public sealed record RegisterWeatherForecastCommand(decimal Temperature, string Location, string Description, DateTime Date);
