using DevConnect.Application.AuthUseCase.Commands;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase.Handlers;

public sealed class RegisterUserCommandHandler : IRegisterUserCommandHandler
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenService _tokenService;

    public RegisterUserCommandHandler(IIdentityService identityService, IJwtTokenService tokenService)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<Result<string>> HandleAsync(RegisterUserCommand command)
    {
        if (command == null)
            return Result<string>.Failure(new Error("Register.InvalidRequest", "Register command is null"));

        var registerDto = new RegisterDto(command.Email, command.Password);
        var (isSuccess, user, errors) = await _identityService.RegisterAsync(registerDto);

        if (!isSuccess)
        {
            return Result<string>.Failure(new Error("Registration.Failed", string.Join("; ", errors)));
        }

        var token = await _tokenService.GenerateTokenAsync(user!);
        return Result<string>.Success(token);
    }
}
