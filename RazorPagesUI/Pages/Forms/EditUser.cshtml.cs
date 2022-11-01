using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;
using RazorPagesUI.Utils;

namespace RazorPagesUI.Pages.Forms
{
    public class EditUserModel : PageModel
    {
        private readonly IDataRepository _dataRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public User EditedUser { get; set; }

        public EditUserModel(IDataRepository dataRepository, IWebHostEnvironment webHostEnvironment)
        {
            _dataRepository = dataRepository;
            _webHostEnvironment = webHostEnvironment;
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
            if (ProfileImage is not null)
            {
                var imagePath = FilesManager.ProcessUploadProfileImage(_webHostEnvironment, ProfileImage);

                User.NewProfileImage = imagePath;

                return Page();
            }

            FilesManager.DeleteExistingProfileImage(_webHostEnvironment, user);

            User = _dataRepository.EditUser(user);

            return RedirectToPage("/Index");
        }
    }
}
