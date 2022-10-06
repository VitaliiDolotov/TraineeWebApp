using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages.Forms
{
    public class DeleteUserModel : PageModel
    {
        [BindProperty]
        public UserModel User { get; set; }

        public IActionResult OnGet(string userName)
        {
            if (!DataStorage.Users.Any(x => x.UserName.Equals(userName)))
            {
                return RedirectToPage("/Index");
            }

            User = DataStorage.Users.First(x => x.UserName.Equals(userName));

            return Page();

        }

        public IActionResult OnPost()
        {
            try
            {
                DataStorage
                    .Users
                    .RemoveAll(x => x.UserName.Equals(User.UserName));
            }
            catch { }


            return RedirectToPage("/Index");
        }
    }
}
