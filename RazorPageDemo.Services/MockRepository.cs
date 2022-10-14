using RazorPagesDemo.Models;

namespace RazorPageDemo.Services
{
    public class MockRepository : IDataRepository
    {
        private List<User> _usersList;

        public MockRepository()
        {
            _usersList = new List<User>()
            {
                new User() {Name = "Oleg", YearOfBirth = 1993},
                new User() { Name = "Dasha", YearOfBirth = 1992 },
                new User() { Name = "Oksana", YearOfBirth = 1996 }
            };
        }

        public void AddUser(User user)
        {
            _usersList.Add(user);
        }

        public void DeleteUser(User user)
        {
            var userToDelete = GetUser(user.Id);

            if (userToDelete is not null)
            {
                try
                {
                    _usersList.RemoveAll(x => x.Id.Equals(userToDelete.Id));
                }
                catch { }
            }
        }

        public User? EditUser(User updatedUser)
        {
            var user = GetUser(updatedUser.Id);

            if (user is not null)
            {
                user.Name = updatedUser.Name;
                user.YearOfBirth = updatedUser.YearOfBirth;
            }

            return null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _usersList;
        }

        public User? GetUser(string id)
        {
            if (_usersList.Any(x => x.Id.Equals(id)))
            {
                return _usersList.First(x => x.Id.Equals(id));
            }

            return null;
        }
    }
}
