using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Net.Http.Headers;
using Tester.UI.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Tester.UI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        builder.Services.AddAuthorizationCore();

        builder.Services.AddHttpClient("Tester.API", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7141/");                                                                                  
        })
        .AddHttpMessageHandler(sp =>
        {
            var authService = sp.GetRequiredService<AuthService>();
            return new AuthorizedHandler(authService);
        });

        builder.Services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return factory.CreateClient("Tester.API");
        });

        await builder.Build().RunAsync();
    }
}
