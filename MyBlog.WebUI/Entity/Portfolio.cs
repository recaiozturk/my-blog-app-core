namespace MyBlog.WebUI.Entity
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PortfolioType { get; set; }
        public string ProjectDate { get; set; } = null!;
        public string? ProjectUrl { get; set; } 
        public string UsedTechnologies { get; set; } = null!;
        public string? Description { get; set; }
        public List<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();
    }
}
