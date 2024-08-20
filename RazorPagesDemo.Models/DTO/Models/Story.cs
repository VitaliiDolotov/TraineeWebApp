namespace RazorPageDemo.Services.Models
{
	public class Story
	{
		public int Id { get; set; }
		public Dictionary<Language, string> Title { get; set; }
		public ImageCollection Images { get; set; }
		public Dictionary<Language, string> Texts { get; set; }
		public Dictionary<Language, string> Voices { get; set; }
		public string MusicFileLocation { get; set; }
	}
}
