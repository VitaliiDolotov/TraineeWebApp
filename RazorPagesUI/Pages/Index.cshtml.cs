using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages
{
    public class IndexModel : PageModel
    {
        public List<AddressModel> Adresses { get; set; }
        public List<UserModel> Users { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            DataStorage.Addresses.RemoveAll(x => x.Created <= DateTime.UtcNow.AddMinutes(-4));
            DataStorage.Users.RemoveAll(x => x.Created <= DateTime.UtcNow.AddMinutes(-4));

            Adresses = DataStorage.Addresses;
            Users = DataStorage.Users;
        }
    }
}