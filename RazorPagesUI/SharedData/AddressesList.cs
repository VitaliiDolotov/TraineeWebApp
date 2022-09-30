using RazorPagesUI.Models;

namespace RazorPagesUI.SharedData
{
	public static class DataStorage
	{
		public static List<AddressModel> Addresses { get; set; }
        public static List<UserModel> Users { get; set; }

        static DataStorage()
		{
            Addresses = new List<AddressModel>();
            Users = new List<UserModel>();
        }
    }
}
