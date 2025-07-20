using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Commands;

public sealed class ChangeUserRoleCommand
{
    public string UserEmail { get; }
    public string NewRole { get; }

    public ChangeUserRoleCommand(string userEmail, string newRole)
    {
        UserEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
        NewRole = newRole ?? throw new ArgumentNullException(nameof(newRole));
    }
}

public interface IChangeUserRoleCommandHandler
{
    Task<Result<string>> HandleAsync(ChangeUserRoleCommand command);
}
