using System;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Application.AuthUseCase;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(RegisterDto registerDto);
    Task<Result<AuthResultDto>> LoginAsync(LoginDto loginDto);
}
