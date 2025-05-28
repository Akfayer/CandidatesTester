using Microsoft.JSInterop;

namespace Tester.UI.Auth;

public class AuthService
{
    private readonly IJSRuntime _js;

    public AuthService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<string?> GetToken() =>
        await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("localStorage.clear");
    }
}
