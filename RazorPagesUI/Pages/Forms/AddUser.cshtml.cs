using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages.Forms
{
    public class AddUserModel : PageModel
    {
        private readonly IDataRepository _dataRepository;

        [BindProperty]
        public User User { get; set; }

        public AddUserModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            if (_dataRepository.GetAllUsers().Any(x => x.Name.Equals(User.Name)) &&
                _dataRepository.GetAllUsers().Any(x => x.YearOfBirth.Equals(User.YearOfBirth)))
            {
                return Page();
            }

            _dataRepository.AddUser(User);

            return RedirectToPage("/Index");
        }
    }
}
