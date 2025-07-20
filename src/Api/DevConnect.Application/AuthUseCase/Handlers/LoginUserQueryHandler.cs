using DevConnect.Application.AuthUseCase.Queries;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Handlers;

public sealed class LoginUserQueryHandler : ILoginUserQueryHandler
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenService _tokenService;

    public LoginUserQueryHandler(IIdentityService identityService, IJwtTokenService tokenService)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<Result<AuthResultDto>> HandleAsync(LoginUserQuery query)
    {
        if (query == null)
            return Result<AuthResultDto>.Failure(new Error("Login.InvalidRequest", "Login query is null"));

        var user = await _identityService.FindByEmailAsync(query.Email);

        if (user == null)
        {
            return Result<AuthResultDto>.Failure(new Error("Login.UserNotFound", "User not found"));
        }

        var isValid = await _identityService.CheckPasswordAsync(user, query.Password);

        if (!isValid)
        {
            return Result<AuthResultDto>.Failure(new Error("Login.InvalidPassword", "Invalid password"));
        }

        var token = await _tokenService.GenerateTokenAsync(user!);
        var authResult = new AuthResultDto(token);

        return Result<AuthResultDto>.Success(authResult);
    }
}
