using DevConnect.Application.AuthUseCase;
using DevConnect.Domain.AuthUseCase;
using DevConnect.Domain.Shared;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace DevConnect.Infrastructure.AuthUseCase;

public class IdentityService(ICommonRepository<ApplicationUser> commonRepository, IUserRepository userRepo, IRoleRepository roleRepository) : IIdentityService
{
    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        var userFromDb = await commonRepository.GetByIdAsync(user.Id);
        if (userFromDb == null)
        {
            // TODO: User not found
            return false;
        }

        var result = passwordHasher.VerifyHashedPassword(user, userFromDb.PasswordHash ?? "", password);
        return result == PasswordVerificationResult.Success;
    }

    public Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return userRepo.FindByEmailAsync(email);
    }

    public async Task<(bool isSuccess, ApplicationUser? user, string[] errors)> RegisterAsync(RegisterDto user)
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        // Buscar el rol por defecto
        var defaultRole = await roleRepository.FindByTypeAsync(UserRole.User);

        var applicationUser = new ApplicationUser
        {
            Email = user.Email,
            UserName = user.Email,
            RoleId = defaultRole?.Id // Asignar rol por defecto
        };
        applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser, user.Password);

        try
        {
            await commonRepository.AddAsync(applicationUser);
        }
        catch (Exception ex)
        {
            return (false, null, new[] { ex.Message });
        }

        return (true, applicationUser, Array.Empty<string>());
    }
}
