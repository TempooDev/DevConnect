namespace DevConnect.Domain.WeatherForecast;

public sealed record Temperature(decimal Value)
{
    public static Temperature FromCelsius(decimal value)
    {
        if (value < -100 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Temperature out of realistic range");
        return new Temperature(value);
    }
}
