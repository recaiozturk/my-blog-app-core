using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EFGenericDal<TEntity> : IGenericDal<TEntity> 
        where TEntity : class 
    {
        private readonly ApplicationDbContext _context;

        public EFGenericDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(true);
        }
    }
}
