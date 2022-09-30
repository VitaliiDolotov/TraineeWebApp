namespace RazorPagesUI.Models
{
	public class UserModel
	{
		public string UserName { get; set; }
		public int YearOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
