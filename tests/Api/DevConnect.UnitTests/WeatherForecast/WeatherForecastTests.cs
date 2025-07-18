





using System;
using DevConnect.Domain.WeatherForecast;
using Xunit;

namespace DevConnect.UnitTests.WeatherForecast;

public sealed class WeatherForecastTests
{
    [Fact]
    public void Register_ShouldCreateForecast_WhenDataIsValid()
    {
        var id = WeatherForecastId.NewId();
        var temp = Temperature.FromCelsius(25);
        var location = "Madrid";
        var description = "Sunny";
        var date = new DateTime(2025, 7, 18);

        var forecast = Domain.WeatherForecast.WeatherForecast.Register(id, temp, location, description, date);

        Assert.Equal(id, forecast.Id);
        Assert.Equal(temp, forecast.Temperature);
        Assert.Equal(location, forecast.Location);
        Assert.Equal(description, forecast.Description);
        Assert.Equal(date, forecast.Date);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Register_ShouldThrow_WhenLocationIsInvalid(string location)
    {
        var id = WeatherForecastId.NewId();
        var temp = Temperature.FromCelsius(20);
        var description = "Cloudy";
        var date = DateTime.Now;

        Assert.Throws<ArgumentException>(() => Domain.WeatherForecast.WeatherForecast.Register(id, temp, location, description, date));
    }
}
