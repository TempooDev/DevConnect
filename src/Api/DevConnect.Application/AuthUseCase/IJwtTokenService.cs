using DevConnect.Domain.AuthUseCase;

namespace DevConnect.Application.AuthUseCase;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}
