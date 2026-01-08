using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;
using RazorPagesDemo.Models.DTO;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages;

public class IndexModel : PageModel
{
    private readonly UserApi _userApi;
    private readonly AddressApi _addressApi;

    public IEnumerable<UserDtoResponse> Users { get; private set; } = Array.Empty<UserDtoResponse>();
    public IEnumerable<Address> Adresses { get; private set; } = Array.Empty<Address>();

    public IndexModel(UserApi userApi, AddressApi addressApi)
    {
        _userApi = userApi;
        _addressApi = addressApi;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var token = this.GetAccessToken();
        var returnUrl = Url.Page(AppRoutes.IndexPage)!;

        var (addresses, addrAction) = await _addressApi.GetAllAsync(
            page: this,
            token: token,
            returnUrl: returnUrl);

        if (addrAction is not null) return addrAction;
        Adresses = addresses ?? Array.Empty<Address>();

        var (users, userAction) = await _userApi.GetAllAsync(
            page: this,
            token: token,
            returnUrl: returnUrl);

        if (userAction is not null) return userAction;
        Users = users ?? Array.Empty<UserDtoResponse>();

        return Page();
    }
}