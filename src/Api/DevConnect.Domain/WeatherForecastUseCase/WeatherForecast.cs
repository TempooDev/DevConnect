namespace DevConnect.Domain.WeatherForecastUseCase;

public sealed class WeatherForecast
{
    public WeatherForecastId Id { get; init; }
    public Temperature Temperature { get; init; }
    public string Location { get; init; }
    public string Description { get; init; }
    public DateTime Date { get; init; }

    // Parameterless constructor for EF Core
    public WeatherForecast() { }

    // Full constructor for domain logic
    public WeatherForecast(WeatherForecastId id, Temperature temperature, string location, string description, DateTime date)
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
