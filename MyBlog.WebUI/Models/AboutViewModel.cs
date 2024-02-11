using MyBlog.WebUI.Entity;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models
{
    public class AboutViewModel
    {
        public int AboutId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string? Website { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Age { get; set; }


        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Adress { get; set; }

        public string? Image { get; set; } = string.Empty;

        public List<Skill>? Skills{ get; set; }

        [Required]
        public string?  Summary { get; set; }

        public string? FavoriteMovie { get; set; }
        public string? FavoriteSerie { get; set; }
        public string? FavoriteMusic { get; set; }
        public string? FavoriteBook { get; set; }
    }
}
