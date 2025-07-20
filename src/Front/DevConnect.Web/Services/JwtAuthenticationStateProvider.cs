using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace DevConnect.Web.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage;

    public CustomAuthenticationStateProvider(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Para simplicidad, asumimos que si hay token, el usuario está autenticado
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "User"),
            new Claim(ClaimTypes.Email, "user@example.com")
        };

        var identity = new ClaimsIdentity(claims, "custom");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticatedAsync(string token)
    {
        await _tokenStorage.SetTokenAsync(token);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "User"),
            new Claim(ClaimTypes.Email, "user@example.com")
        };

        var identity = new ClaimsIdentity(claims, "custom");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        await _tokenStorage.RemoveTokenAsync();
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}
