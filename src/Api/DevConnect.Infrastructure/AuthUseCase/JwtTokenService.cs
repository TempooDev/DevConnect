using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevConnect.Application.AuthUseCase;
using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevConnect.Infrastructure.AuthUseCase;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IRoleRepository _roleRepository;

    public JwtTokenService(IConfiguration configuration, IRoleRepository roleRepository)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<string> GenerateTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var jwtSettings = _configuration.GetSection("Jwt");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured");
        var issuer = jwtSettings["Issuer"] ?? "DevConnect";
        var audience = jwtSettings["Audience"] ?? "DevConnect";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Obtener el rol del usuario desde la BD
        var userRole = await GetUserRoleAsync(user, cancellationToken);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Role, userRole)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // 1 hora de duración
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GetUserRoleAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        // Si el usuario tiene un rol asignado, buscarlo en la BD
        if (user.RoleId != null)
        {
            var role = await _roleRepository.GetByIdAsync(user.RoleId, cancellationToken);
            if (role != null && role.IsActive)
            {
                return role.Name;
            }
        }

        // Si no tiene rol asignado o el rol no existe, buscar el rol por defecto
        var defaultRole = await _roleRepository.FindByTypeAsync(UserRole.User, cancellationToken);
        return defaultRole?.Name ?? UserRole.User.ToStringValue();
    }
}
