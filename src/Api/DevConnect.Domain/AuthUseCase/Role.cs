using DevConnect.Shared.Enums;

namespace DevConnect.Domain.AuthUseCase;

public sealed class Role
{
    public RoleId Id { get; private set; }
    public UserRole Type { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private Role(RoleId id, UserRole type, string name, string description)
    {
        Id = id;
        Type = type;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static Role Create(RoleId id, UserRole type, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Role description cannot be empty", nameof(description));

        return new Role(id, type, name, description);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty", nameof(newDescription));

        Description = newDescription;
    }
}
