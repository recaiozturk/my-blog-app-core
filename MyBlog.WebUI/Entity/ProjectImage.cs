namespace MyBlog.WebUI.Entity
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;
    }
}
