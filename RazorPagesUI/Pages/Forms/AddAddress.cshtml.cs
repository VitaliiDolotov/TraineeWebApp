using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages.Forms
{
    public class AddAddressModel : PageModel
    {
        private readonly IDataRepository _dataRepository;

        [BindProperty]
        public Address Address { get; set; }

        public AddAddressModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_dataRepository.GetAllAddresses().Any(x => x.StreetAddress.Equals(Address.StreetAddress) &&
            x.City.Equals(Address.City) &&
            x.State.Equals(Address.State) &&
            x.ZipCode.Equals(Address.ZipCode)))
            {
                return Page();
            }

            _dataRepository.AddAddress(Address);

            return RedirectToPage("/Index");
        }
    }
}
