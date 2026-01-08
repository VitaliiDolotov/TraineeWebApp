namespace RazorPagesDemo.Models.DTO
{
	public class UserDtoResponse
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int YearOfBirth { get; set; }
		public Gender Gender { get; set; }
		public DateTime Created { get; set; }
		public string? ProfileImage { get; set; } 
	}
}
