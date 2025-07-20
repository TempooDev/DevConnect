using DevConnect.Domain.Shared;

namespace DevConnect.Domain.AuthUseCase;

public interface IUserRepository : ICommonRepository<ApplicationUser>
{
    Task<ApplicationUser?> FindByEmailAsync(string email);
}
