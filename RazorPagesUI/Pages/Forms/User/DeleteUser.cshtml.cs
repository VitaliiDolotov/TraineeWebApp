using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models.DTO;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages.Forms.User;

public class DeleteUserModel : PageModel
{
    private readonly UserApi _userApi;

    public new UserDtoResponse? User { get; private set; }

    [BindProperty] public string Id { get; set; } = null!;

    public DeleteUserModel(UserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Id = id;

        var token = this.GetAccessToken();
        var (user, action) = await _userApi.GetByIdAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteUser}/{id}",
            id: id);

        if (action is not null) return action;

        User = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var token = this.GetAccessToken();

        var result = await _userApi.DeleteAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteUser}/{Id}",
            id: Id);

        if (result is not null) return result;

        var (user, _) = await _userApi.GetByIdAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteUser}/{Id}",
            id: Id);

        User = user;
        return Page();
    }
}