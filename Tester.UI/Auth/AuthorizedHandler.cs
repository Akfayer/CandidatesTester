using System.Net.Http.Headers;

namespace Tester.UI.Auth;

public class AuthorizedHandler : DelegatingHandler
{
    private readonly AuthService _authService;

    public AuthorizedHandler(AuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _authService.GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}