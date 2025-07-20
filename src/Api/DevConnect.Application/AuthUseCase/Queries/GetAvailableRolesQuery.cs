using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Queries;

public sealed class GetAvailableRolesQuery
{
    // Empty query - no parameters needed
}

public interface IGetAvailableRolesQueryHandler
{
    Task<Result<IReadOnlyList<string>>> HandleAsync(GetAvailableRolesQuery query);
}
