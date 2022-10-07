using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages.Forms
{
    public class EditUserModel : PageModel
    {
        [BindProperty]
        public UserModel User { get; set; }

        [BindProperty]
        public UserModel EditedUser { get; set; }

        public IActionResult OnGet(string id)
        {
            if (!DataStorage.Users.Any(x => x.Id.Equals(id)))
            {
                return RedirectToPage("/Index");
            }

            User = DataStorage.Users.First(x => x.Id.Equals(id));

            return Page();

        }

        public IActionResult OnPost()
        {
            try
            {
                var user = DataStorage
                    .Users
                    .First(x => x.Id.Equals(User.Id));
                user.UserName = EditedUser.UserName;
                user.YearOfBirth = EditedUser.YearOfBirth;
            }
            catch { }


            return RedirectToPage("/Index");
        }
    }
}
