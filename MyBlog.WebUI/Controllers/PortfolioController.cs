using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebUI.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
