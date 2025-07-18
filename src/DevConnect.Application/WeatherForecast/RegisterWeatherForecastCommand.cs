namespace DevConnect.Application.WeatherForecast;

public sealed record RegisterWeatherForecastCommand(decimal Temperature, string Location, string Description, DateTime Date);
