using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IContactDal:IGenericDal<Contact>
    {
        Task<List<Contact>> GetAllMessages();
    }
}
