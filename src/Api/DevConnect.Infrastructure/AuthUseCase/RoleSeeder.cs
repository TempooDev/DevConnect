using DevConnect.Domain.AuthUseCase;
using DevConnect.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Infrastructure.AuthUseCase;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(ApplicationDbContext context)
    {
        if (await context.DevConnectRoles.AnyAsync())
        {
            return; // Los roles ya están sembrados
        }

        var roles = new[]
        {
            Role.Create(
                new RoleId(1),
                UserRole.User,
                "User",
                "Usuario estándar con permisos básicos del sistema"
            ),
            Role.Create(
                new RoleId(2),
                UserRole.Administrator,
                "Administrator",
                "Administrador con acceso completo al sistema"
            )
        };

        await context.DevConnectRoles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}
