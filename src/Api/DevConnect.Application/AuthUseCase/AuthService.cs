using System;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase;

public class AuthService(IIdentityService identityService, IJwtTokenService tokenService)
    : IAuthService
{
    public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
    {
        var (isSuccess, user, errors) = await identityService.RegisterAsync(registerDto);

        if (!isSuccess)
        {
            return Result<string>.Failure(new Error("Registration.Failed", string.Join("; ", errors)));
        }

        var token = await tokenService.GenerateTokenAsync(user!);
        return Result<string>.Success(token);
    }

    public async Task<Result<AuthResultDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await identityService.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return Result<AuthResultDto>.Failure(new Error("Login.UserNotFound", "User not found"));
        }

        var isValid = await identityService.CheckPasswordAsync(user, loginDto.Password);

        if (!isValid)
        {
            return Result<AuthResultDto>.Failure(new Error("Login.InvalidPassword", "Invalid password"));
        }

        var token = await tokenService.GenerateTokenAsync(user!);
        var authResult = new AuthResultDto(token);

        return Result<AuthResultDto>.Success(authResult);
    }
}
