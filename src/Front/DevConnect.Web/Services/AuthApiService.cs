using System.Net.Http.Headers;
using System.Net.Http.Json;
using DevConnect.Shared.DTOs.AuthUseCase;
using DevConnect.Shared.Result;

namespace DevConnect.Web.Services;

public interface IAuthApiService
{
    Task<Result<string>> RegisterAsync(RegisterDto registerDto);
    Task<Result<AuthResultDto>> LoginAsync(LoginDto loginDto);
    Task<Result<IReadOnlyList<string>>> GetAvailableRolesAsync();
    Task<Result<string>> ChangeUserRoleAsync(ChangeUserRoleDto changeRoleDto);
}

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorage _tokenStorage;

    public AuthApiService(HttpClient httpClient, ITokenStorage tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return Result<string>.Success(token.Trim('"')); // Remove quotes from JSON string
            }

            return Result<string>.Failure(new Error("Registration.Failed", $"Registration failed: {response.StatusCode}"));
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(new Error("Registration.Error", ex.Message));
        }
    }

    public async Task<Result<AuthResultDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResultDto>();
                return Result<AuthResultDto>.Success(authResult!);
            }

            return Result<AuthResultDto>.Failure(new Error("Login.Failed", $"Login failed: {response.StatusCode}"));
        }
        catch (Exception ex)
        {
            return Result<AuthResultDto>.Failure(new Error("Login.Error", ex.Message));
        }
    }

    public async Task<Result<IReadOnlyList<string>>> GetAvailableRolesAsync()
    {
        try
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("/api/user-roles/available-roles");

            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadFromJsonAsync<List<string>>();
                return Result<IReadOnlyList<string>>.Success(roles?.AsReadOnly() ?? new List<string>().AsReadOnly());
            }

            return Result<IReadOnlyList<string>>.Failure(new Error("GetRoles.Failed", $"Failed to get roles: {response.StatusCode}"));
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<string>>.Failure(new Error("GetRoles.Error", ex.Message));
        }
    }

    public async Task<Result<string>> ChangeUserRoleAsync(ChangeUserRoleDto changeRoleDto)
    {
        try
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.PutAsJsonAsync("/api/user-roles/change-role", changeRoleDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Result<string>.Success(result.Trim('"'));
            }

            return Result<string>.Failure(new Error("ChangeRole.Failed", $"Failed to change role: {response.StatusCode}"));
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(new Error("ChangeRole.Error", ex.Message));
        }
    }

    private async Task SetAuthorizationHeaderAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
