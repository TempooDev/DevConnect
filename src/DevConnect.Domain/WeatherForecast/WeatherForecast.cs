namespace DevConnect.Domain.WeatherForecast;

public sealed class WeatherForecast
{
    public WeatherForecastId Id { get; }
    public Temperature Temperature { get; }
    public string Location { get; }
    public string Description { get; }
    public DateTime Date { get; }

    private WeatherForecast(WeatherForecastId id, Temperature temperature, string location, string description, DateTime date)
    {
        Id = id;
        Temperature = temperature;
        Location = location;
        Description = description;
        Date = date;
    }

    public static WeatherForecast Register(WeatherForecastId id, Temperature temperature, string location, string description, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty", nameof(location));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        return new WeatherForecast(id, temperature, location, description, date);
    }
}
