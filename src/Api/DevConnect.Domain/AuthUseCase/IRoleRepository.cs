using DevConnect.Shared.Enums;
using DevConnect.Domain.Shared;

namespace DevConnect.Domain.AuthUseCase;

public interface IRoleRepository : ICommonRepository<Role>
{
    Task<Role?> FindByTypeAsync(UserRole type, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> FindAllActiveAsync(CancellationToken cancellationToken = default);
}
