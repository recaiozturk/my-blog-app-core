using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.Models;
using System.Reflection;
using static MyBlog.WebUI.Enums.Enums;

namespace MyBlog.WebUI.ViewComponents
{
    public class PortfolioViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int portfolioType,int pageType)
        {
            PortfolioViewModel viewModel = new PortfolioViewModel();

            string porfolioTitle = "";
            string porfolioIdString = "";

            if (portfolioType == ((int)PorfolioType.Porfolio))
                porfolioTitle = "Portfölyö";
            else if (portfolioType == ((int)PorfolioType.Prototypes))
                porfolioTitle = "Demo ve Prototiplerim";

            //singlePage de header linkleri sorunu için 
            //if (pageType == ((int)PageType.SinglePage))
            //    porfolioIdString = "portfolio";
            //else if (pageType == ((int)PageType.NormalPage))
            //    porfolioIdString = "portfolioDetailed";

            viewModel.PortfolioType = portfolioType;
            viewModel.PorfolioTitle = porfolioTitle;
            //viewModel.PageTypeStringForId = porfolioIdString;

            return View(viewModel);
        }
    }
}
