using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Experience> Experiences { get; set; }
    }
}
