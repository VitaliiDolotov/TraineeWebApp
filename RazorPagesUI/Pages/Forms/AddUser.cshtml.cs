using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages.Forms
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //if (DataStorage.Users.Any(x => x.UserName.Equals(User.UserName)) &&
            //    DataStorage.Users.Any(x => x.YearOfBirth.Equals(User.YearOfBirth)))
            //{
            //    return Page();
            //}

            //DataStorage.Users.Add(User);

            return RedirectToPage("/Index");
        }
    }
}
