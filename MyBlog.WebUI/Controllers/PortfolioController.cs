using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebUI.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult AllPortfolios()
        {
            return View();
        }
    }
}
