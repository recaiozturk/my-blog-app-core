using Azure;
using MyBlog.WebUI.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.WebUI.Entity
{
    public class Experience
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DateBetween { get; set; }
        public string? Adress { get; set; }
        public string? UniversityName { get; set; }
        public string? Description { get; set; }
        public string? CompanyName { get; set; }
        public string? ExperienceSteps { get; set; }
        public int DisplayOrder { get; set; }


        [NotMapped]
        public List<string> ExpStepsSentences => StaticMethods.SplitSentences(ExperienceSteps ?? "");

    }
}
