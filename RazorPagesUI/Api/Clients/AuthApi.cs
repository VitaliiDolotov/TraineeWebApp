using System.Net;

namespace RazorPagesUI.Api.Clients;

public sealed class AuthApi
{
    private readonly IHttpClientFactory _factory;
    public AuthApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<(string? AccessToken, string? Error)> LoginAsync(string username, string password)
    {
        var client = _factory.CreateClient("Api");
        var resp = await client.PostAsJsonAsync("api/Auth/login", new { username, password });

        if (resp.StatusCode == HttpStatusCode.Unauthorized)
        {
            return (null, "Invalid username or password.");
        }

        if (!resp.IsSuccessStatusCode)
        {
            return (null, $"Login failed: {(int)resp.StatusCode} {resp.StatusCode}.");
        }

        var json = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        if (json is null || string.IsNullOrWhiteSpace(json.AccessToken))
            return (null, "Login failed: empty token response.");

        return (json.AccessToken, null);
    }

    private sealed record LoginResponse(string AccessToken, string TokenType);
}