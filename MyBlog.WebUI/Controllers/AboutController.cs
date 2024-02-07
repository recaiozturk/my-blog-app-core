using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutDal _aboutRepository;

        public AboutController(IAboutDal aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<IActionResult>Edit()
        {
            var model  = await _aboutRepository.GetAboutAsync();
            return View(new AboutViewModel
            {
                Adress = model.Adress,
                Age = model.Age,
                Birthday = model.Birthday,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                Website = model.Website
            });
        }
    }
}
