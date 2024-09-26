using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        private readonly IAboutDal _aboutRepository;
        private readonly IMapper _mapper;

        public AboutViewComponent(IAboutDal aboutRepository, IMapper mapper)
        {
            _aboutRepository = aboutRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entity = _aboutRepository.GetAboutAsync();

            if (entity.Result == null)
                return View(new AboutViewModel());

            var model = _mapper.Map<AboutViewModel>(entity.Result);
            return View(  model );

        }
    }
}
