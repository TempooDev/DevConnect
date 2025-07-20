using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.DTOs.AuthUseCase;

namespace DevConnect.Application.AuthUseCase;

public interface IIdentityService
{
    Task<(bool isSuccess, ApplicationUser? user, string[] errors)> RegisterAsync(RegisterDto user);
    Task<ApplicationUser?> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
}
