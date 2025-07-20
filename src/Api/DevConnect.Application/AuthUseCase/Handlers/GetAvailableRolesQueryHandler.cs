using DevConnect.Application.AuthUseCase.Queries;
using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Handlers;

public sealed class GetAvailableRolesQueryHandler : IGetAvailableRolesQueryHandler
{
    private readonly IRoleRepository _roleRepository;

    public GetAvailableRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<Result<IReadOnlyList<string>>> HandleAsync(GetAvailableRolesQuery query)
    {
        var roles = await _roleRepository.FindAllActiveAsync();
        var roleNames = roles.Select(r => r.Name).ToList().AsReadOnly();

        return Result<IReadOnlyList<string>>.Success(roleNames);
    }
}
