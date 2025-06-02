using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Tester.UI.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthStateProvider(AuthService authService)
    {
        _authService = authService;
        _authService.OnAuthStateChanged += NotifyAuthChanged;
    }

    private void NotifyAuthChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _authService.GetToken();
        var identity = new ClaimsIdentity();

        if (!string.IsNullOrWhiteSpace(token))
        {
            var role = await _authService.GetRole();
            var userName = await _authService.GetUserName();

            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName ?? "User"),
                new Claim(ClaimTypes.Role, role ?? "User")
            }, "CustomAuth");
        }

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }
}
