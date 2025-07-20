using DevConnect.Application.AuthUseCase.Commands;
using DevConnect.Application.AuthUseCase.Queries;
using DevConnect.Shared.DTOs.AuthUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.ApiService.AuthUseCase;

public static class UserRoleEndpoints
{
    public static void MapUserRoleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var roleGroup = endpoints.MapGroup("/api/user-roles")
            .WithTags("User Role Management")
            .RequireAuthorization(); // Requiere autenticación

        roleGroup.MapPut("/change-role", ChangeUserRoleAsync)
            .WithName("ChangeUserRole")
            .WithSummary("Change user role (Admin only)")
            .RequireAuthorization("Administrator") // Solo administradores
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden);

        roleGroup.MapGet("/available-roles", GetAvailableRolesAsync)
            .WithName("GetAvailableRoles")
            .WithSummary("Get all available roles")
            .Produces<IReadOnlyList<string>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> ChangeUserRoleAsync(
        ChangeUserRoleDto changeRoleDto,
        IChangeUserRoleCommandHandler commandHandler)
    {
        var command = new ChangeUserRoleCommand(changeRoleDto.UserEmail, changeRoleDto.RoleName);
        var result = await commandHandler.HandleAsync(command);

        if (result.IsFailure)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Change role failed",
                Detail = result.Error?.Message ?? "Change role failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetAvailableRolesAsync(
        IGetAvailableRolesQueryHandler queryHandler)
    {
        var query = new GetAvailableRolesQuery();
        var result = await queryHandler.HandleAsync(query);

        if (result.IsFailure)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Get roles failed",
                Detail = result.Error?.Message ?? "Get roles failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Results.Ok(result.Value);
    }
}
