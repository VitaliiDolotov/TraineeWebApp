using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemo.Models
{
	public class Address
	{
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(5), MaxLength(30)]
        public string StreetAddress { get; set; }

        [Required]
        [MinLength(3), MaxLength(15)]
        public string City { get; set; }

        [Required]
        [MinLength(2), MaxLength(15)]
        public string State { get; set; }

        [Required]
        [MinLength(4), MaxLength(20)]
        [RegularExpression(@"(^\d{5}$)|(^\d{9}$)|(^\d{5}-\d{4}$)")]
        public string ZipCode { get; set; }

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}
