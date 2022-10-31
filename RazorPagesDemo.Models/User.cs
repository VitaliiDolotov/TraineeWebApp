using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemo.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Name is requried")]
        [MinLength(3, ErrorMessage = "Name is too short"), MaxLength(14, ErrorMessage = "Name is too long")]
        public string Name { get; set; }

        [Display(Name = "Year of Birth")]
        [Required(ErrorMessage = "Year of Birth is requried")]
        [Range(1900, 2004, ErrorMessage = "Not valid Year of Birth is set")]
        public int YearOfBirth { get; set; }

        public Gender Gender { get; set; }

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}