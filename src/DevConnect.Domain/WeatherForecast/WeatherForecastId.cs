namespace DevConnect.Domain.WeatherForecast;

public sealed record WeatherForecastId(Guid Value)
{
    public static WeatherForecastId NewId() => new(Guid.NewGuid());
}
