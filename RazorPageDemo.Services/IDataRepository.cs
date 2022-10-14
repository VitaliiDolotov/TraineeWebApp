using RazorPagesDemo.Models;

namespace RazorPageDemo.Services
{
    public interface IDataRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetUser(string id);
        User? EditUser(User user);
        void DeleteUser(User user);
    }
}