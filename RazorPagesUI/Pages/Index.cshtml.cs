using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;
using RazorPagesUI.Models;
using RazorPagesUI.SharedData;

namespace RazorPagesUI.Pages
{
    public class IndexModel : PageModel
    {
        public List<AddressModel> Adresses { get; set; }
        public IEnumerable<User> Users { get; set; }
        private readonly IDataRepository _dataRepository;

        public IndexModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void OnGet()
        {
            Users = _dataRepository.GetAllUsers();
            DataStorage.Addresses.RemoveAll(x => x.Created <= DateTime.UtcNow.AddMinutes(-4));
            //DataStorage.Users.RemoveAll(x => x.Created <= DateTime.UtcNow.AddMinutes(-4));

            Adresses = DataStorage.Addresses;
            //Users = DataStorage.Users;
        }
    }
}