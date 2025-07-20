using DevConnect.Shared.Enums;

namespace DevConnect.Shared.Examples;

/// <summary>
/// Ejemplo de cómo usar y extender el enum UserRole
/// </summary>
public static class UserRoleExamples
{
    /// <summary>
    /// Demuestra el uso básico del enum
    /// </summary>
    public static void BasicUsage()
    {
        // Usando el enum directamente
        var adminRole = UserRole.Administrator;
        var userRole = UserRole.User;

        // Convirtiendo a string
        var adminString = adminRole.ToStringValue(); // "Administrator"
        var userString = userRole.ToStringValue();   // "User"

        // Convirtiendo desde string
        var roleFromString = UserRoleExtensions.FromString("Administrator");
    }    /// <summary>
         /// Ejemplo de cómo verificar permisos usando el enum
         /// </summary>
    public static bool HasAdminPermissions(UserRole userRole)
    {
        return userRole == UserRole.Administrator;
    }

    /// <summary>
    /// Ejemplo de cómo obtener todos los roles disponibles
    /// </summary>
    public static UserRole[] GetAllRoles()
    {
        return Enum.GetValues<UserRole>();
    }

    /// <summary>
    /// Ejemplo de extensión futura: agregar más roles
    /// Para agregar un nuevo rol, simplemente agrega una entrada al enum:
    /// 
    /// public enum UserRole
    /// {
    ///     Usuario = 0,
    ///     Administrator = 1,
    ///     Moderator = 2,      // Nuevo rol
    ///     SuperAdmin = 3      // Otro nuevo rol
    /// }
    /// 
    /// Y actualiza las extensiones en UserRoleExtensions
    /// </summary>
    public static string GetRoleDescription(UserRole role) => role switch
    {
        UserRole.User => "Usuario estándar con permisos básicos",
        UserRole.Administrator => "Administrador con permisos completos",
        _ => "Rol desconocido"
    };
}
