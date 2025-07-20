using Microsoft.AspNetCore.Identity;

namespace DevConnect.Domain.AuthUseCase;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public RoleId? RoleId { get; set; }
    public Role? Role { get; set; }
}