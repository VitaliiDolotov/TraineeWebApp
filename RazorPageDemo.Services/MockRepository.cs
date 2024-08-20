using RazorPageDemo.Services.Models;
using RazorPagesDemo.Models;

namespace RazorPageDemo.Services
{
	public class MockRepository : IDataRepository
	{
		private List<User> _usersList;
		private List<Address> _addressesList;
		private List<Story> _storiesList;
		private readonly string _driveBaseUrl = "https://drive.usercontent.google.com/download?id=";

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

			_storiesList = GeneratStories();
		}

		private List<Story> GeneratStories()
		{
			return new List<Story>()
			{
				new Story()
				{
					Id = 1,
					Title  = new Dictionary<Language, string>()
					{
						{ Language.GB, "Glimmer's Glow" },
						{ Language.FR, "L'Éclat de Glimmer" },
						{ Language.UA, "Сяйво Гліммер" }
					},
					Images = new ImageCollection()
					{
						ImageUrls = new List<string>()
						{
							$"{_driveBaseUrl}1DlbCnSol3mlBE-Xj4JMjzyrgnHtVTiWr",
							$"{_driveBaseUrl}1hm1cH58PFyugGH224Y5P64zDpban3JyR",
						}
					},
					Texts = new Dictionary<Language, string>()
					{
						{ Language.GB, "In the heart of the Whispering Woods lived Glimmer, a tiny green creature with eyes like shiny gems. She loved helping her friends, the flowers and animals. When a dark cloud covered the woods, Glimmer bravely led it back to the sky, using her kindness. With the cloud gone, the forest shone brighter, and Glimmer was hailed as the tiny hero who brought back the light." },
						{ Language.FR, "Au cœur des Bois Murmurants vivait Glimmer, une petite créature verte aux yeux brillants comme des joyaux. Elle adorait aider ses amis, les fleurs et les animaux. Quand un nuage sombre recouvrit la forêt, Glimmer le guida courageusement vers le ciel avec sa gentillesse. Une fois le nuage parti, la forêt resplendit de plus belle, et Glimmer fut célébrée comme la minuscule héroïne qui ramena la lumière." },
						{ Language.UA, "У серці Шепочучого Лісу жила Гліммер, маленька зелена істота з очима, як блискучі коштовності. Вона любила допомагати своїм друзям, квітам та тваринам. Коли темна хмара покрила ліс, Гліммер відважно вела її назад до неба, використовуючи свою доброту. З хмарою пішла, ліс засяяв яскравіше, і Гліммер була проголошена маленькою героїнею, яка повернула світло." }
					},
					Voices = new Dictionary<Language, string>()
					{
						{ Language.GB, $"{_driveBaseUrl}1PG29_ulTp3bmqbHsk7xVm_0RYCjg83ls" },
						{ Language.FR, $"{_driveBaseUrl}1v45Vy0uwOhuHRaJwp93AV30gQjWZQa_n" },
						{ Language.UA, $"{_driveBaseUrl}1vNPsTcL-46M1e6v30aZDS76FowbaEwmA" }
					},
					MusicFileLocation = $"{_driveBaseUrl}1l7vU_wmfQXqhPAXLsfih3UEGiWrV1CcY",
				},
				new Story()
				{
					Id = 2,
					Title  = new Dictionary<Language, string>()
					{
						{ Language.GB, "Mouse's Birthday Bash" },
						{ Language.FR, "La Fête d'Anniversaire de la Souris" },
						{ Language.UA, "День Народження Мишеня" }
					},
					Images = new ImageCollection()
					{
						ImageUrls = new List<string>()
						{
							$"{_driveBaseUrl}1OIHA7RYiiSnuaaFg96Z-VAVi_ICteo2F",
							$"{_driveBaseUrl}1tG6ttxfLaBgIoXUDXPEszJJXDSVaLfu3",
						}
					},
					Texts = new Dictionary<Language, string>()
					{
						{ Language.GB, "In a cozy room filled with colorful balloons and laughter, Mouse celebrated his birthday. He wore a party hat and grinned, holding a wrapped gift that sparkled with mystery. Around him, a sea of balls in every hue created a playground of joy. It was a day of fun, friends, and sweet surprises for everyone's favorite cheerful Mouse." },
						{ Language.FR, "Dans une pièce chaleureuse, décorée de ballons colorés et de rires, la Souris fêtait son anniversaire. Coiffé d'un chapeau de fête, il tenait un cadeau emballé, scintillant de mystère. Autour de lui, une mer de balles de toutes les couleurs formait un terrain de jeu joyeux. C'était un jour de divertissement, d'amis et de douces surprises pour la Souris enjouée." },
						{ Language.UA, "У затишній кімнаті, наповненій кольоровими кульками та сміхом, Миша святкував свій день народження. На ньому був веселий капелюшок, і він усміхався, тримаючи загадковий подарунок. Навколо нього море кульок усіх кольорів створювало радісний ігровий простір. Це був день розваг, друзів та солодких сюрпризів для улюбленої веселої Миші." }
					},
					Voices = new Dictionary<Language, string>()
					{
						{ Language.GB, $"{_driveBaseUrl}1YI1gg8E9p2yuglxsO2GPkFGbuIE58i_A" },
						{ Language.FR, $"{_driveBaseUrl}1hJO5Hxc6e4WcRcn_A1BqLED8gMg905g5" },
						{ Language.UA, $"{_driveBaseUrl}1KIAWn76QjywsGD1lq5OSAJNYl-MDL4Hh" }
					},
					MusicFileLocation = $"{_driveBaseUrl}1F3VhOxmze1WZKdV58H2WaVYUbCUMVxlG",
				},
				new Story()
				{
					Id = 3,
					Title  = new Dictionary<Language, string>()
					{
						{ Language.GB, "The Enchanted Elixir of Luminara" },
						{ Language.FR, "L'Élixir Enchanté de Luminara" },
						{ Language.UA, "Зачарований Еліксир Люмінари" }
					},
					Images = new ImageCollection()
					{
						ImageUrls = new List<string>()
						{
							$"{_driveBaseUrl}1_E4s248JzhCFfNdf78c2RMiB9qpj7Q6e",
							$"{_driveBaseUrl}16o0y6sKPIspNoXcWfgloBjSiC3DiTGgN",
							$"{_driveBaseUrl}18BrxYKoWsVWrd4xg1WfW4papfTqIs3L_",
						}
					},
					Texts = new Dictionary<Language, string>()
					{
						{ Language.GB, "The Enchanted Elixir of Luminara. In the heart of the Enchanted Forest, where trees whispered secrets and rivers sang melodies, there lived a fairy named Liora. Her wings shimmered like the morning dew, and her hair was as white as the moonlight. Liora was no ordinary fairy; she was the Keeper of the Glowing Elixir, a magical potion that could heal any wound or illness in the forest." },
						{ Language.FR, "Au cœur de la Forêt Enchantée, où les arbres murmuraient des secrets et les rivières chantaient des mélodies, vivait une fée nommée Liora. Ses ailes brillaient comme la rosée du matin, et ses cheveux étaient aussi blancs que le clair de lune. Liora n'était pas une fée ordinaire ; elle était la Gardienne de l'Élixir Lumineux, une potion magique qui pouvait guérir toute blessure ou maladie dans la forêt." },
						{ Language.UA, "У серці Зачарованого Лісу, де дерева шепотіли секрети та річки співали мелодії, жила фея на ім'я Ліора. Її крила іскрились, немов ранкова роса, а волосся було білим, як місячне світло. Ліора не була звичайною феєю; вона була Хранителькою Сяючого Еліксиру, магічного зілля, яке могло вилікувати будь-яку рану чи хворобу в лісі." }
					},
					Voices = new Dictionary<Language, string>()
					{
						{ Language.GB, $"{_driveBaseUrl}1L5DeIQbjXNkkquzb8vjX_sFQVvHWLgLY" },
						{ Language.FR, $"{_driveBaseUrl}1o5KgY-xpmzVwedJHpqv24S5EmxAavOPd" },
						{ Language.UA, $"{_driveBaseUrl}1KCyHV7nSyFYN0j1_hjYHooWMa5Kb7CJd" }
					},
					MusicFileLocation = $"{_driveBaseUrl}1hn7FuGAnuIoDAJT2pCTVcT5jButKXfr1",
				}
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

		#region Story management

		public IEnumerable<Story> GetAllStories()
		{
			return _storiesList;
		}

		public Story? GetStoryById(int id)
		{
			if (_storiesList.Any(x => x.Id.Equals(id)))
			{
				return _storiesList.First(x => x.Id.Equals(id));
			}

			return null;
		}

		#endregion
	}
}
