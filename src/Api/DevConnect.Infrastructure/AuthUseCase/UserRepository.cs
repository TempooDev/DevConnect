using DevConnect.Domain.AuthUseCase;
using DevConnect.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.AuthUseCase;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICommonRepository<ApplicationUser> _commonRepository;

    public UserRepository(ApplicationDbContext dbContext, ICommonRepository<ApplicationUser> commonRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _commonRepository = commonRepository ?? throw new ArgumentNullException(nameof(commonRepository));
    }

    // Método específico del dominio
    public Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    // Delegación a CommonRepository para operaciones CRUD
    public Task AddAsync(ApplicationUser entity, CancellationToken cancellationToken = default)
        => _commonRepository.AddAsync(entity, cancellationToken);

    public Task UpdateAsync(ApplicationUser entity, CancellationToken cancellationToken = default)
        => _commonRepository.UpdateAsync(entity, cancellationToken);

    public Task DeleteAsync(ApplicationUser entity, CancellationToken cancellationToken = default)
        => _commonRepository.DeleteAsync(entity, cancellationToken);

    public Task<ApplicationUser?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        => _commonRepository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default)
        => _commonRepository.GetAllAsync(cancellationToken);
}
