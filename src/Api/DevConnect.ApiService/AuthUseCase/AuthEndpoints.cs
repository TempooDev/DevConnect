using DevConnect.Application.AuthUseCase.Commands;
using DevConnect.Application.AuthUseCase.Queries;
using DevConnect.Shared.DTOs.AuthUseCase;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.ApiService.AuthUseCase;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var authGroup = endpoints.MapGroup("/api/auth")
            .WithTags("Authentication");

        authGroup.MapPost("/register", RegisterAsync)
            .WithName("Register")
            .WithSummary("Register a new user")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        authGroup.MapPost("/login", LoginAsync)
            .WithName("Login")
            .WithSummary("Login user")
            .Produces<AuthResultDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> RegisterAsync(
        RegisterDto registerDto,
        IRegisterUserCommandHandler commandHandler)
    {
        var command = new RegisterUserCommand(registerDto.Email, registerDto.Password, registerDto.Password);
        var result = await commandHandler.HandleAsync(command);

        if (result.IsFailure)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Registration failed",
                Detail = result.Error?.Message ?? "Registration failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> LoginAsync(
        LoginDto loginDto,
        ILoginUserQueryHandler queryHandler)
    {
        var query = new LoginUserQuery(loginDto.Email, loginDto.Password);
        var result = await queryHandler.HandleAsync(query);

        if (result.IsFailure)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Login failed",
                Detail = result.Error?.Message ?? "Login failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Results.Ok(result.Value);
    }
}
