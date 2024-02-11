using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        private readonly IAboutDal _aboutRepository;

        public AboutViewComponent(IAboutDal aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entity = _aboutRepository.GetAboutAsync();

            if (entity.Result == null)
                return View(new AboutViewModel());

            return View(new AboutViewModel
            {
                Adress=entity.Result.Adress,
                Age=entity.Result.Age,
                Birthday=entity.Result.Birthday,
                Email=entity.Result.Email,
                PhoneNumber=entity.Result.PhoneNumber,
                Title=entity.Result.Title,
                Website = entity.Result.Website,
                Skills=entity.Result.Skills,
                Summary=entity.Result.Summary,
                FavoriteBook=entity.Result.FavoriteBook,
                FavoriteMusic=entity.Result.FavoriteMusic,
                FavoriteSerie=entity.Result.FavoriteSerie,
                FavoriteMovie=entity.Result.FavoriteMovie,
                Image=entity.Result.Image,
            });
        }
    }
}
