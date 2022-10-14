using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Address> Adresses { get; set; }
        public IEnumerable<User> Users { get; set; }
        private readonly IDataRepository _dataRepository;

        public IndexModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void OnGet()
        {
            foreach (var user in _dataRepository.GetAllUsers().ToList())
            {
                if (user.Created <= DateTime.UtcNow.AddMinutes(-4))
                {
                    _dataRepository.DeleteUser(user);
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