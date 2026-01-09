using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages.Forms.Address;

public class DeleteAddressModel : PageModel
{
    private readonly AddressApi _addressApi;

    public RazorPagesDemo.Models.Address? Address { get; private set; }

    [BindProperty] public string Id { get; set; } = null!;

    public DeleteAddressModel(AddressApi addressApi)
    {
        _addressApi = addressApi;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Id = id;

        var token = this.GetAccessToken();
        var (addr, action) = await _addressApi.GetByIdAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteAddress}/{id}",
            id: id);

        if (action is not null) return action;

        Address = addr;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var token = this.GetAccessToken();

        var result = await _addressApi.DeleteAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteAddress}/{Id}",
            id: Id);

        if (result is not null) return result;

        var (addr, _) = await _addressApi.GetByIdAsync(
            page: this,
            token: token,
            returnUrl: $"{AppRoutes.DeleteAddress}/{Id}",
            id: Id);

        Address = addr;
        return Page();
    }
}