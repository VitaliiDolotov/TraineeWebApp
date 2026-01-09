using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models.DTO;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages.Forms.User;

public class AddUserModel : PageModel
{
    private readonly UserApi _userApi;

    [BindProperty] public new RazorPagesDemo.Models.User User { get; set; } = new();

    public AddUserModel(UserApi userApi)
    {
        _userApi = userApi;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var dto = new UserDtoRequest
        {
            Name = User.Name,
            YearOfBirth = User.YearOfBirth,
            Gender = User.Gender
        };

        var token = this.GetAccessToken();
        var result = await _userApi.CreateAsync(
            page: this,
            token: token,
            returnUrl: AppRoutes.AddUser,
            dto: dto);

        return result ?? Page();
    }
}