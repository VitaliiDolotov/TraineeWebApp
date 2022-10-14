using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages.Forms
{
    public class DeleteUserModel : PageModel
    {
        private readonly IDataRepository _dataRepository;

        [BindProperty]
        public User User { get; set; }

        public DeleteUserModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult OnGet(string id)
        {
            User = _dataRepository.GetUser(id);

            if (User is null)
            {
                return RedirectToPage("/Index");
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            _dataRepository.DeleteUser(User);

            return RedirectToPage("/Index");
        }
    }
}
