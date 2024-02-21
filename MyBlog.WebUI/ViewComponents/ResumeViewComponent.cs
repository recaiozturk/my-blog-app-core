using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Models.Resume;
using MyBlog.WebUI.Util;

namespace MyBlog.WebUI.ViewComponents
{
    public class ResumeViewComponent: ViewComponent
    {
        private readonly IEducationDal _educationDal;
        private readonly IExperienceDal _experienceDal;
        private readonly IAboutDal _aboutDal;

        public ResumeViewComponent(IEducationDal educationDal, IExperienceDal experienceDal, IAboutDal aboutDal)
        {
            _educationDal = educationDal;
            _experienceDal = experienceDal;
            _aboutDal = aboutDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ResumeViewModel model = new ResumeViewModel();

            try
            {
                var about = await _aboutDal.GetAboutAsync();
                var edusl = await _educationDal.GetAll().ToListAsync();
                var exps= await _experienceDal.GetAll().OrderBy(x => x.DisplayOrder).ToListAsync();

                model.Experiences = exps;
                model.Educations = edusl;
                model.About = about;


                return View(model);
            }
            catch (Exception ex)
            {
                //temp ile hata gösterilebilir
                return View(model);
            }
        }

    }
}
