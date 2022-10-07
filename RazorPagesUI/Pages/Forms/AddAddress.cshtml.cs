using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages.Forms
{
    public class AddAddressModel : PageModel
    {
        [BindProperty]
        public AddressModel Address { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            if (DataStorage.Addresses.Any(x => x.StreetAddress.Equals(Address.StreetAddress) &&
            x.City.Equals(Address.City) &&
            x.State.Equals(Address.State) &&
            x.ZipCode.Equals(Address.ZipCode)))
            {
                return Page();
            }

            DataStorage.Addresses.Add(Address);

            return RedirectToPage("/Index");
        }
    }
}
