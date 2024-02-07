using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IAboutDal:IGenericDal<About>
    {
        Task<About> GetAboutAsync();
    }
}
