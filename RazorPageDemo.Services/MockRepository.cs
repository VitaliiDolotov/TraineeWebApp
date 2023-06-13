using RazorPagesDemo.Models;

namespace RazorPageDemo.Services
{
	public class MockRepository : IDataRepository
	{
		private List<User> _usersList;
		private List<Address> _addressesList;

		public MockRepository()
		{
			_usersList = new List<User>()
			{
				new User() { Name = "Oleg", YearOfBirth = 1993, Created = DateTime.UtcNow.AddYears(-1), Gender = Gender.Male, ProfileImage = "oleg.jpg" },
				new User() { Name = "Dasha", YearOfBirth = 1992, Created = DateTime.UtcNow.AddMonths(-1), Gender = Gender.Female, ProfileImage = "dasha.jpg" },
				new User() { Name = "Oksana", YearOfBirth = 1996, Created = DateTime.UtcNow.AddDays(-6), Gender = Gender.Female, ProfileImage = "oksana.jpg" },
				new User() { Name = "Vitalii", YearOfBirth = 1990, Created = DateTime.UtcNow.AddDays(-5), Gender = Gender.Male, ProfileImage = "vitalii.jpg" }
			};

			_addressesList = new List<Address>()
			{
				new Address() { StreetAddress = "178 Broadway", City = "New York", State = "NY", ZipCode = "11211", Created = DateTime.UtcNow},
				new Address() { StreetAddress = "201 W Austin Blvd", City = "Nevada", State = "MO", ZipCode = "64772", Created = DateTime.UtcNow},
				new Address() { StreetAddress = "205 E Houston St", City = "New York", State = "NY", ZipCode = "10002", Created = DateTime.UtcNow},
			};
		}

		#region User management

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

		public User? EditUser(string id, User updatedUser)
		{
			var user = GetUser(id);

			if (user is not null)
			{
				user.Name = updatedUser.Name;
				user.YearOfBirth = updatedUser.YearOfBirth;
				user.Gender = updatedUser.Gender;
				user.ProfileImage = !string.IsNullOrEmpty(updatedUser.NewProfileImage) ? updatedUser.NewProfileImage : user.ProfileImage;
				user.NewProfileImage = null;
			}

			return user;
		}

		public User? EditUserProfileImage(User user, string image)
		{
			var userToUpdate = GetUser(user.Id);

			if (userToUpdate is null)
			{
				return null;
			}

			if (user is not null && !string.IsNullOrEmpty(image))
			{
				userToUpdate.NewProfileImage = image;
			}

			return userToUpdate;
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

		#endregion

		#region Address management

		public Address? GetAddress(string id)
		{
			if (_addressesList.Any(x => x.Id.Equals(id)))
			{
				return _addressesList.First(x => x.Id.Equals(id));
			}

			return null;
		}

		public IEnumerable<Address> GetAllAddresses()
		{
			return _addressesList;
		}

		public void AddAddress(Address address)
		{
			_addressesList.Add(address);
		}

		public void DeleteAddress(Address address)
		{
			var addressToDelete = GetAddress(address.Id);

			if (addressToDelete is not null)
			{
				try
				{
					_addressesList.RemoveAll(x => x.Id.Equals(addressToDelete.Id));
				}
				catch { }
			}
		}

		#endregion
	}
}
