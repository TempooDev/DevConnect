using DevConnect.Application.AuthUseCase.Commands;
using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Handlers;

public sealed class ChangeUserRoleCommandHandler : IChangeUserRoleCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public ChangeUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<Result<string>> HandleAsync(ChangeUserRoleCommand command)
    {
        if (command == null)
            return Result<string>.Failure(new Error("ChangeRole.InvalidRequest", "Change role command is null"));

        if (string.IsNullOrWhiteSpace(command.UserEmail))
            return Result<string>.Failure(new Error("ChangeRole.InvalidUserEmail", "User email cannot be empty"));

        if (string.IsNullOrWhiteSpace(command.NewRole))
            return Result<string>.Failure(new Error("ChangeRole.InvalidRoleName", "Role name cannot be empty"));

        // Buscar el usuario por email
        var user = await _userRepository.FindByEmailAsync(command.UserEmail);
        if (user == null)
        {
            return Result<string>.Failure(new Error("ChangeRole.UserNotFound", "User not found"));
        }

        // Buscar el rol por nombre
        var roles = await _roleRepository.FindAllActiveAsync();
        var targetRole = roles.FirstOrDefault(r => r.Name.Equals(command.NewRole, StringComparison.OrdinalIgnoreCase));

        if (targetRole == null)
        {
            return Result<string>.Failure(new Error("ChangeRole.RoleNotFound", $"Role '{command.NewRole}' not found"));
        }

        // Actualizar el rol del usuario
        user.RoleId = targetRole.Id;
        await _userRepository.UpdateAsync(user);

        return Result<string>.Success($"User role changed to '{targetRole.Name}' successfully");
    }
}
