using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfExperienceDal:EFGenericDal<Experience>, IExperienceDal
    {
        private readonly ApplicationDbContext _context;
        public EfExperienceDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
