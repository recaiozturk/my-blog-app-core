using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.Util;

namespace MyBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAboutComponent()
        {
            var componentHtml = await this.RenderViewComponentToStringAsync("About");
            return Json(new { html = componentHtml });
        }

        public async Task<IActionResult> LoadResumeComponent()
        {
            var componentHtml = await this.RenderViewComponentToStringAsync("Resume");
            return Json(new { html = componentHtml });
        }

        public async Task<IActionResult> LoadPortfolioComponent()
        {
            var componentHtml = await this.RenderViewComponentToStringAsync("Portfolio");
            return Json(new { html = componentHtml });
        }

        public async Task<IActionResult> LoadContactComponent()
        {
            var componentHtml = await this.RenderViewComponentToStringAsync("Contact");
            return Json(new { html = componentHtml });
        }





    }
}