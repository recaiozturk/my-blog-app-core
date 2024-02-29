using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Contact
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Subject alanı zorunludur.")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
    }
}
