namespace DevConnect.Application.WeatherForecastUseCase;

public sealed record WeatherForecastDto(
    Guid Id,
    DateTime Date,
    string Location,
    decimal Temperature,
    string Description
);
