namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IGenericDal<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetById(int id);

        IQueryable<TEntity> GetAll();

        Task Update(TEntity entity);
    }
}
