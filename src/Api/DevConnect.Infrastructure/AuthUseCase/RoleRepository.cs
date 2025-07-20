using DevConnect.Domain.AuthUseCase;
using DevConnect.Domain.Shared;
using DevConnect.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.AuthUseCase;

public sealed class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICommonRepository<Role> _commonRepository;

    public RoleRepository(ApplicationDbContext dbContext, ICommonRepository<Role> commonRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _commonRepository = commonRepository ?? throw new ArgumentNullException(nameof(commonRepository));
    }

    // Métodos específicos del dominio
    public async Task<Role?> FindByTypeAsync(UserRole type, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DevConnectRoles
            .FirstOrDefaultAsync(r => r.Type == type && r.IsActive, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> FindAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DevConnectRoles
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    // Delegación a CommonRepository para operaciones CRUD
    public Task AddAsync(Role entity, CancellationToken cancellationToken = default)
        => _commonRepository.AddAsync(entity, cancellationToken);

    public Task UpdateAsync(Role entity, CancellationToken cancellationToken = default)
        => _commonRepository.UpdateAsync(entity, cancellationToken);

    public Task DeleteAsync(Role entity, CancellationToken cancellationToken = default)
        => _commonRepository.DeleteAsync(entity, cancellationToken);

    public Task<Role?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        => _commonRepository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        => _commonRepository.GetAllAsync(cancellationToken);
}
