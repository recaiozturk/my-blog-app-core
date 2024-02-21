using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.Models.Resume
{
    public class ResumeViewModel
    {
        public List<Education> Educations{ get; set; } = new List<Education>();
        public List<Experience> Experiences { get; set; }= new List<Experience>();
        public About? About { get; set; }
    }
}
