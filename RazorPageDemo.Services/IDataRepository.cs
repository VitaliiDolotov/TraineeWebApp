using RazorPagesDemo.Models;

namespace RazorPageDemo.Services
{
    public interface IDataRepository
    {
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        User? GetUser(string id);
        User? EditUser(User user);
        void DeleteUser(User user);

        IEnumerable<Address> GetAllAddresses();
        void AddAddress(Address address);
        Address? GetAddress(string id);
        void DeleteAddress(Address address);
    }
}