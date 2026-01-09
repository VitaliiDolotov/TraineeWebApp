using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesUI.Infrastructure.Auth;

public static class PageModelAuthExtensions
{
    public static string? GetAccessToken(this PageModel page)
    {
        if (!page.Request.Cookies.TryGetValue(AuthConstants.AccessTokenCookie, out var token))
            return null;

        if (string.IsNullOrWhiteSpace(token))
            return null;

        token = token.Trim();

        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = token["Bearer ".Length..].Trim();
        }

        return string.IsNullOrWhiteSpace(token) ? null : token;
    }
}
