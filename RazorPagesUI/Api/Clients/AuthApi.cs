namespace RazorPagesUI.Api.Clients;

public sealed class AuthApi
{
    private readonly IHttpClientFactory _factory;
    public AuthApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<(string? AccessToken, string? Error)> LoginAsync(string username, string password)
    {
        var client = _factory.CreateClient("Api");
        var resp = await client.PostAsJsonAsync("api/Auth/login", new { username, password });

        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync();
            return (null, $"Login failed: {(int)resp.StatusCode} {resp.StatusCode}. {body}");
        }

        var json = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        return (json?.AccessToken, null);
    }

    private sealed record LoginResponse(string AccessToken, string TokenType);
}
