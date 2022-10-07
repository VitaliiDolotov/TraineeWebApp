using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages.Forms
{
    public class DeleteAddressModel : PageModel
    {
        [BindProperty]
        public AddressModel Address { get; set; }

        public IActionResult OnGet(string id)
        {
            if (!DataStorage.Addresses.Any(x => x.Id.Equals(id)))
            {
                return RedirectToPage("/Index");
            }

            Address = DataStorage.Addresses.First(x => x.Id.Equals(id));

            return Page();

        }

        public IActionResult OnPost()
        {
            try
            {
                DataStorage
                    .Addresses
                    .RemoveAll(x => x.Id.Equals(Address.Id));
            }
            catch { }


            return RedirectToPage("/Index");
        }
    }
}
