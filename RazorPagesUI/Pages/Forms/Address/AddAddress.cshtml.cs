using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Pages.Forms.Address;

public class AddAddressModel : PageModel
{
    private readonly AddressApi _addressApi;

    [BindProperty] public RazorPagesDemo.Models.Address Address { get; set; } = new();

    public AddAddressModel(AddressApi addressApi)
    {
        _addressApi = addressApi;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var token = this.GetAccessToken();
        var result = await _addressApi.CreateAsync(
            page: this,
            token: token,
            returnUrl: AppRoutes.AddAddress,
            address: Address);

        return result ?? Page();
    }
}