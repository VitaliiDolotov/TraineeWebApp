using System.ComponentModel.DataAnnotations;

namespace RazorPagesDemo.Models
{
    public class Address
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street Address is required")]
        [MinLength(5, ErrorMessage = "Street Address is too short"), MaxLength(30, ErrorMessage = "Street Address is too long")]
        public string StreetAddress { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        [MinLength(3, ErrorMessage = "City is too short"), MaxLength(15, ErrorMessage = "City is too long")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        [MinLength(2, ErrorMessage = "State is too short"), MaxLength(15, ErrorMessage = "State is too long")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required")]
        [MinLength(4, ErrorMessage = "Zip Code is too short"), MaxLength(20, ErrorMessage = "Zip Code is too long")]
        [RegularExpression(@"(^\d{5}$)|(^\d{9}$)|(^\d{5}-\d{4}$)", ErrorMessage = "Zip Code is incorrect")]
        public string ZipCode { get; set; }

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}
