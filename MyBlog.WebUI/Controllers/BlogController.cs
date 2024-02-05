using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebUI.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
