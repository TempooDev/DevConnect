namespace DevConnect.Domain.WeatherForecastUseCase;

public sealed record WeatherForecastId
{
    public Guid Value { get; init; }

    // Parameterless constructor for EF Core
    public WeatherForecastId() { }

    public WeatherForecastId(Guid value)
    {
        Value = value;
    }

    public static WeatherForecastId NewId() => new(Guid.NewGuid());
}
