using Microsoft.JSInterop;

namespace DevConnect.Web.Services;

public interface ITokenStorage
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token);
    Task RemoveTokenAsync();
}

public class LocalStorageTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "devconnect_token";

    public LocalStorageTokenStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }
        catch
        {
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }
        catch
        {
            // Handle errors silently for now
        }
    }

    public async Task RemoveTokenAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
        catch
        {
            // Handle errors silently for now
        }
    }
}
