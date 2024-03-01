using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } = true;
    }
}
