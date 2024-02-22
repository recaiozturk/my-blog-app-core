using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IPortfolioDal:IGenericDal<Portfolio>
    {
        Task<List<Portfolio>> GetPortfolios();
    }
}
