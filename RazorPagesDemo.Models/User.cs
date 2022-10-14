using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemo.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        //[BindProperty]
        //[Required]
        //[MinLength(3), MaxLength(14)]
        public string Name { get; set; }

        //[BindProperty]
        //[Required]
        //[Range(1900, 2004)]
        public int YearOfBirth { get; set; }

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}