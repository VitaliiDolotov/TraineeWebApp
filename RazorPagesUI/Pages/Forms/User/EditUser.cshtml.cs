using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models.DTO;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Files;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages.Forms.User;

public class EditUserModel : PageModel
{
    private readonly UserApi _userApi;
    private readonly IWebHostEnvironment _webHostEnvironment;

    [BindProperty] public IFormFile? ProfileImage { get; set; }
    [BindProperty] public new RazorPagesDemo.Models.User User { get; set; } = new();

    public EditUserModel(UserApi userApi, IWebHostEnvironment webHostEnvironment)
    {
        _userApi = userApi;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var token = this.GetAccessToken();
        var (dto, action) = await _userApi.GetByIdAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.EditUser}/{id}",
            id: id);

        if (action is not null) return action;
        if (dto is null) return RedirectToPage(AppRoutes.IndexPage);

        User = new RazorPagesDemo.Models.User
        {
            Id = dto.Id,
            Name = dto.Name,
            YearOfBirth = dto.YearOfBirth,
            Gender = dto.Gender,
            ProfileImage = dto.ProfileImage
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (ProfileImage is not null)
        {
            var imagePath = FilesManager.ProcessUploadProfileImage(_webHostEnvironment, ProfileImage);
            User.NewProfileImage = imagePath;
            return Page();
        }

        var dto = new UserDtoRequest
        {
            Name = User.Name,
            YearOfBirth = User.YearOfBirth,
            Gender = User.Gender
        };

        var token = this.GetAccessToken();
        var result = await _userApi.UpdateAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.EditUser}/{id}",
            id: id,
            dto: dto);

        return result ?? Page();
    }
}
