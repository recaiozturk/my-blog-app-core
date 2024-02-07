using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfAboutDal : EFGenericDal<About>, IAboutDal
    {
        public EfAboutDal(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<About> GetAboutAsync()
        {
            return await GetAll().FirstAsync();
        }
    }
}
