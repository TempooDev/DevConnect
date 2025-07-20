using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase;

public interface IUserRoleService
{
    Task<Result<string>> ChangeUserRoleAsync(ChangeUserRoleDto changeRoleDto, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<string>>> GetAvailableRolesAsync(CancellationToken cancellationToken = default);
}
