using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesUI.Models
{
    public class AddressModel
    {
        [BindProperty]
        [Required]
        [MinLength(8), MaxLength(30)]
        public string StreetAddress { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3), MaxLength(15)]
        public string City { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3), MaxLength(15)]
        public string State { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3), MaxLength(20)]
        [RegularExpression(@"(^\d{5}$)|(^\d{9}$)|(^\d{5}-\d{4}$)")]
        public string ZipCode { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}