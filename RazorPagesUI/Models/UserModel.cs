using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesUI.Models
{
    public class UserModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [BindProperty]
        [Required]
        [MinLength(3), MaxLength(14)]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [Range(1900, 2004)]
        public int YearOfBirth { get; set; }

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }
}
