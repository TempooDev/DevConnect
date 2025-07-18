using DevConnect.Domain.WeatherForecast;
using Xunit;

namespace DevConnect.UnitTests.WeatherForecast;

public sealed class TemperatureTests
{
    [Theory]
    [InlineData(-100)]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void FromCelsius_ShouldCreateTemperature_WhenValueIsValid(decimal value)
    {
        var temp = Temperature.FromCelsius(value);
        Assert.Equal(value, temp.Value);
    }

    [Theory]
    [InlineData(-101)]
    [InlineData(101)]
    public void FromCelsius_ShouldThrow_WhenValueIsOutOfRange(decimal value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Temperature.FromCelsius(value));
    }
}
