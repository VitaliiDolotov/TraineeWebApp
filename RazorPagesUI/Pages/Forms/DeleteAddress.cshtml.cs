using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages.Forms
{
    public class DeleteAddressModel : PageModel
    {
        private readonly IDataRepository _dataRepository;

        [BindProperty]
        public Address Address { get; set; }

        public DeleteAddressModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult OnGet(string id)
        {
            Address = _dataRepository.GetAddress(id);

            if (Address is null)
            {
                return RedirectToPage("/Index");
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            _dataRepository.DeleteAddress(Address);

            return RedirectToPage("/Index");
        }
    }
}
