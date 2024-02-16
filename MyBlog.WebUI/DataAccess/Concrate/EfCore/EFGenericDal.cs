using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

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

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(true);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
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

        public async Task<TEntity> ReturnUpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(true);
            return entity;
        }
    }
}
