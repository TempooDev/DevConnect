
using DevConnect.Infrastructure.WeatherForecastUseCase;
using DevConnect.Application.WeatherForecastUseCase;
using DevConnect.Domain.WeatherForecastUseCase;
using Microsoft.EntityFrameworkCore;
using DevConnect.ApiService.WeatherForecastUseCase;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add DbContext
builder.Services.AddDbContext<WeatherForecastDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("devconnectdb")));

// Register repository and handlers
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<FindWeatherForecastsHandler>();

builder.Services.AddScoped<IRegisterWeatherForecastHandler, RegisterWeatherForecastHandler>();
builder.Services.AddScoped<IFindWeatherForecastsHandler, FindWeatherForecastsHandler>();


var app = builder.Build();

// Apply database migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WeatherForecastDbContext>();
    db.Database.Migrate();
}


app.MapDefaultEndpoints();
app.UseHttpsRedirection();
// Register WeatherForecast endpoints
app.MapWeatherForecastEndpoints();
app.Run();
