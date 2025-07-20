namespace DevConnect.Shared.Enums;

public enum UserRole
{
    User = 0,
    Administrator = 1
}

public static class UserRoleExtensions
{
    public static string ToStringValue(this UserRole role) => role switch
    {
        UserRole.User => "User",
        UserRole.Administrator => "Administrator",
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };

    public static UserRole FromString(string roleString) => roleString switch
    {
        "User" => UserRole.User,
        "Administrator" => UserRole.Administrator,
        _ => throw new ArgumentException($"Invalid role: {roleString}", nameof(roleString))
    };
}
