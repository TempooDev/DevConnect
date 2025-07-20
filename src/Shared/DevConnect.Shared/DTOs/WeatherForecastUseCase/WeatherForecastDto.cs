namespace DevConnect.Shared.DTOs.WeatherForecastUseCase;

public sealed record WeatherForecastDto(
    Guid Id,
    DateTime Date,
    string Location,
    decimal Temperature,
    string Description
);
