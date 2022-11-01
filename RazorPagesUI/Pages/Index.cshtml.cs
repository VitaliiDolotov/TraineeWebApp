using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;
using RazorPagesUI.Utils;

namespace RazorPagesUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataRepository _dataRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IEnumerable<Address> Adresses { get; set; }
        public IEnumerable<User> Users { get; set; }

        public IndexModel(IDataRepository dataRepository, IWebHostEnvironment webHostEnvironment)
        {
            _dataRepository = dataRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            foreach (var user in _dataRepository.GetAllUsers().ToList())
            {
                if (user.Created <= DateTime.UtcNow.AddMinutes(-4))
                {
                    _dataRepository.DeleteUser(user);
                    FilesManager.ClearProfileImagesFolder(_webHostEnvironment);
                }
            }

            foreach (var address in _dataRepository.GetAllAddresses().ToList())
            {
                if (address.Created <= DateTime.UtcNow.AddMinutes(-4))
                {
                    _dataRepository.DeleteAddress(address);
                }
            }

            Users = _dataRepository.GetAllUsers();
            Adresses = _dataRepository.GetAllAddresses();
        }
    }
}