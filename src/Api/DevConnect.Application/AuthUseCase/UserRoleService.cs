using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserRoleService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<Result<string>> ChangeUserRoleAsync(ChangeUserRoleDto changeRoleDto, CancellationToken cancellationToken = default)
    {
        if (changeRoleDto == null)
            return Result<string>.Failure(new Error("ChangeRole.InvalidRequest", "Change role request is null"));

        if (string.IsNullOrWhiteSpace(changeRoleDto.UserEmail))
            return Result<string>.Failure(new Error("ChangeRole.InvalidUserEmail", "User email cannot be empty"));

        if (string.IsNullOrWhiteSpace(changeRoleDto.RoleName))
            return Result<string>.Failure(new Error("ChangeRole.InvalidRoleName", "Role name cannot be empty"));

        // Buscar el usuario
        var user = await _userRepository.FindByEmailAsync(changeRoleDto.UserEmail);
        if (user == null)
        {
            return Result<string>.Failure(new Error("ChangeRole.UserNotFound", "User not found"));
        }

        // Buscar el rol por nombre
        var roles = await _roleRepository.FindAllActiveAsync(cancellationToken);
        var targetRole = roles.FirstOrDefault(r => r.Name.Equals(changeRoleDto.RoleName, StringComparison.OrdinalIgnoreCase));

        if (targetRole == null)
        {
            return Result<string>.Failure(new Error("ChangeRole.RoleNotFound", $"Role '{changeRoleDto.RoleName}' not found"));
        }

        // Actualizar el rol del usuario
        user.RoleId = targetRole.Id;
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result<string>.Success($"User role changed to '{targetRole.Name}' successfully");
    }

    public async Task<Result<IReadOnlyList<string>>> GetAvailableRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleRepository.FindAllActiveAsync(cancellationToken);
        var roleNames = roles.Select(r => r.Name).ToList().AsReadOnly();

        return Result<IReadOnlyList<string>>.Success(roleNames);
    }
}
