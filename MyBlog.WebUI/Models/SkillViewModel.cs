using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models
{
    public class SkillViewModel
    {
        public int AboutId { get; set; }

        public int SkillId { get; set; }

        [Required]
        public string? SkillName { get; set; }

        [Required]
        [Range(1,100)]
        public int SkillValue { get; set; }
    }
}
