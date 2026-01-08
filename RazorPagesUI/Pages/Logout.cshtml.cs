using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet(string? returnUrl = null)
    {
        Response.Cookies.Delete(AuthConstants.AccessTokenCookie);

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToPage(AppRoutes.LoginPage);
    }
}
