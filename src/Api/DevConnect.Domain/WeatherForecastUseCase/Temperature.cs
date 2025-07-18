namespace DevConnect.Domain.WeatherForecastUseCase;


public sealed record Temperature
{
    public decimal Value { get; init; }

    // Parameterless constructor for EF Core
    public Temperature() { }

    public Temperature(decimal value)
    {
        Value = value;
    }

    public static Temperature FromCelsius(decimal value)
    {
        if (value < -100 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Temperature out of realistic range");
        return new Temperature(value);
    }
}
