using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfProjectImageDal : EFGenericDal<ProjectImage>, IProjectImageDal
    {
        private readonly ApplicationDbContext _context;
        public EfProjectImageDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
