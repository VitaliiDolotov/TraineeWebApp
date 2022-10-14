using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages.Forms
{
    public class EditUserModel : PageModel
    {
        private readonly IDataRepository _dataRepository;

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public User EditedUser { get; set; }

        public EditUserModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult OnGet(string id)
        {
            var user = _dataRepository.GetUser(id);

            if (user is null)
            {
                return RedirectToPage("/Index");
            }

            User = user;

            return Page();

        }

        public IActionResult OnPost(User user)
        {
            User = _dataRepository.EditUser(user);

            return RedirectToPage("/Index");
        }
    }
}
