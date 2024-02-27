using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;

namespace MyBlog.WebUI.ViewComponents
{
    public class PortfolioViewComponent: ViewComponent
    {
        private readonly IPortfolioDal _portfolioDal;

        public PortfolioViewComponent(IPortfolioDal portfolioDal)
        {
            _portfolioDal = portfolioDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _portfolioDal.GetPortfolios();
            return View(portfolios);
        }
    }
}
