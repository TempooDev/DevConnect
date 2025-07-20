using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Commands;

public sealed class RegisterUserCommand
{
    public string Email { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }

    public RegisterUserCommand(string email, string password, string confirmPassword)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        ConfirmPassword = confirmPassword ?? throw new ArgumentNullException(nameof(confirmPassword));
    }
}

public interface IRegisterUserCommandHandler
{
    Task<Result<string>> HandleAsync(RegisterUserCommand command);
}
