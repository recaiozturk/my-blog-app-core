using MyBlog.WebUI.Util;
namespace MyBlog.WebUI.Models
{
    public class TempPorrtfolioDetailModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AppType { get; set; }
        public string AppTypeStr => ((Enums.AppType)AppType).ToString();

        public string? TempCreatedDate { get; set; }
        public string? ProjectUrl { get; set; }
        public string? ProjectDetail { get; set; }
        public string?  UsedTechnologies { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }

    }
}