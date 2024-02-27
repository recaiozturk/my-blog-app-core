using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IPortfolioDal:IGenericDal<Portfolio>
    {
        Task<List<Portfolio>> GetPortfolios();

        Task<Portfolio> GetPortfolioById(int id);

        Task DeletePortfolioWithImages(int id);
        Task ResetPortfolioImagesIsCover(int id);

    }
}
