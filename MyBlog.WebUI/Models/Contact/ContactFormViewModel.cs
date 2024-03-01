using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Contact
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Konu alanı zorunludur.")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public string Message { get; set; } = null!;
    }
}
