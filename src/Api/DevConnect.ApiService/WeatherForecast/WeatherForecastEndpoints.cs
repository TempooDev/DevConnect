using DevConnect.Application.WeatherForecastUseCase;
using DevConnect.Domain.WeatherForecastUseCase;
using DevConnect.Infrastructure.WeatherForecastUseCase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DevConnect.ApiService.WeatherForecastUseCase;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapGet("/weatherforecasts", async (IFindWeatherForecastsHandler handler, DateTime? date, string? location) =>
        {
            var query = new FindWeatherForecastsQuery(date ?? default, location ?? string.Empty);
            var result = await handler.HandleAsync(query, CancellationToken.None);
            return Results.Ok(result);
        });

        app.MapPost("/weatherforecasts", async (IRegisterWeatherForecastHandler handler, RegisterWeatherForecastCommand command) =>
        {
            await handler.HandleAsync(command, CancellationToken.None);
            return Results.Created($"/weather-forecasts", null);
        });

        return app;
    }
}
