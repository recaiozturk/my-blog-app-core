using MyBlog.WebUI.Util;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.Portfolio
{
    public class PortfolioViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public int PortfolioType { get; set; }

        [Required]
        public string ProjectDate { get; set; } = null!;
        public string? ProjectUrl { get; set; }
        public string UsedTechnologies { get; set; } = null!;
        public string? Description { get; set; }

        public List<int> enumAppTypeList=> Enum.GetValues(typeof(Enums.AppType))
                                       .Cast<int>()
                                       .ToList();

        public List<string>? ProjectImages { get; set; }
    }
}
