using Microsoft.JSInterop;

namespace Tester.UI.Auth;

public class AuthService
{
    private readonly IJSRuntime _js;
        public event Action? OnAuthStateChanged;

    public AuthService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<string?> GetToken() =>
        await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

    public async Task<string?> GetRole() =>
        await _js.InvokeAsync<string>("localStorage.getItem", "userRole");

    public async Task<string?> GetUserName() =>
        await _js.InvokeAsync<string>("localStorage.getItem", "userName");

    public async Task<bool> IsAuthenticated()
    {
        var token = await GetToken();
        return !string.IsNullOrEmpty(token);
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("localStorage.clear");
        NotifyAuthenticationChanged();
    }

    public void NotifyAuthenticationChanged() => OnAuthStateChanged?.Invoke();
}
