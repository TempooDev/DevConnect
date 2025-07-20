using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Queries;

public sealed class LoginUserQuery
{
    public string Email { get; }
    public string Password { get; }

    public LoginUserQuery(string email, string password)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}

public interface ILoginUserQueryHandler
{
    Task<Result<AuthResultDto>> HandleAsync(LoginUserQuery query);
}
