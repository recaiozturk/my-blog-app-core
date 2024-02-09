namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IGenericDal<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);

        IQueryable<TEntity> GetAll();

        Task Update(TEntity entity);
    }
}
