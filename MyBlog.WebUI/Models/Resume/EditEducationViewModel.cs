using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Resume
{
    public class EditEducationViewModel
    {
        public int EducationId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? DateBetween { get; set; }
        [Required]
        public string? Adress { get; set; }
        [Required]
        public string? UniversityName { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
