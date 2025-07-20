
using DevConnect.Infrastructure.WeatherForecastUseCase;
using DevConnect.Application.WeatherForecastUseCase;
using DevConnect.Domain.WeatherForecastUseCase;
using Microsoft.EntityFrameworkCore;
using DevConnect.ApiService.WeatherForecastUseCase;
using DevConnect.Infrastructure;
using DevConnect.Domain.Shared;
using DevConnect.Infrastructure.Shared;
using DevConnect.Application.AuthUseCase;
using DevConnect.Application.AuthUseCase.Commands;
using DevConnect.Application.AuthUseCase.Queries;
using DevConnect.Application.AuthUseCase.Handlers;
using DevConnect.Infrastructure.AuthUseCase;
using DevConnect.Domain.AuthUseCase;
using DevConnect.ApiService.AuthUseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("devconnectdb")));

// Register repository and handlers
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddScoped<FindWeatherForecastsHandler>();

builder.Services.AddScoped<IRegisterWeatherForecastHandler, RegisterWeatherForecastHandler>();
builder.Services.AddScoped<IFindWeatherForecastsHandler, FindWeatherForecastsHandler>();

// Register Auth services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
// Register CQRS Handlers
builder.Services.AddScoped<IChangeUserRoleCommandHandler, ChangeUserRoleCommandHandler>();
builder.Services.AddScoped<IGetAvailableRolesQueryHandler, GetAvailableRolesQueryHandler>();
builder.Services.AddScoped<IRegisterUserCommandHandler, RegisterUserCommandHandler>();
builder.Services.AddScoped<ILoginUserQueryHandler, LoginUserQueryHandler>();

// Legacy services for backward compatibility (if needed)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "DevConnect",
            ValidAudience = jwtSettings["Audience"] ?? "DevConnect",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
        policy.RequireRole("Administrator"));
});


var app = builder.Build();

// Apply database migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    // Seed roles
    await DevConnect.Infrastructure.AuthUseCase.RoleSeeder.SeedRolesAsync(db);
}


app.MapDefaultEndpoints();
app.UseHttpsRedirection();

// Configure authentication middleware
app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapWeatherForecastEndpoints();
app.MapAuthEndpoints();
app.MapUserRoleEndpoints();

app.Run();
