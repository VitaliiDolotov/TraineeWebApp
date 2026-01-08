using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages;

public class LoginModel : PageModel
{
    private readonly AuthApi _authApi;

    public LoginModel(AuthApi authApi)
    {
        _authApi = authApi;
    }

    [BindProperty, Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [BindProperty, Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var (accessToken, error) = await _authApi.LoginAsync(Username, Password);

        if (error != null)
        {
            ModelState.AddModelError(string.Empty, error);
            return Page();
        }

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            ModelState.AddModelError(string.Empty, "Login failed: empty access token.");
            return Page();
        }

        Response.Cookies.Append(
            AuthConstants.AccessTokenCookie,
            accessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(AuthConstants.TokenLifetimeMinutes)
            });

        if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            return Redirect(ReturnUrl);

        return RedirectToPage(AppRoutes.IndexPage);
    }
}