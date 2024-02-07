using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models
{
    public class AboutViewModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Age { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Adress { get; set; }
    }
}
