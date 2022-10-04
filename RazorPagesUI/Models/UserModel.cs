using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesUI.Models
{
    public class UserModel
    {
        [BindProperty]
        [Required]
        [MinLength(6), MaxLength(14)]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [Range(1900, 2004)]
        public int YearOfBirth { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
