using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Resume
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? DateBetween { get; set; }

        [Required]
        public string? Adress { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? CompanyName { get; set; }

        public int DisplayOrder { get; set; }

        public string? ExperienceSteps { get; set; }

    }
}
