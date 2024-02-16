using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfEducationDal:EFGenericDal<Education>,IEducationDal
    {
        private readonly ApplicationDbContext _context;
        public EfEducationDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
