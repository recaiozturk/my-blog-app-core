using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Models.Resume;
using MyBlog.WebUI.Util.Abstract;

namespace MyBlog.WebUI.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IEducationDal _educationDal;
        private readonly IMethods _methods;
        public ResumeController(IEducationDal educationDal, IMethods methods)
        {
            _educationDal = educationDal;
            _methods = methods;
        }

        public async Task<IActionResult> Education()
        {
            ResumeViewModel model= new ResumeViewModel();
            model.Educations=_educationDal.GetAll().ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(EditEducationViewModel model)
        {
            Education educationToAdd = new Education();
            List<string> allErrors = new List<string>();
            bool valid = false;
            if (!ModelState.IsValid)
            {
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                educationToAdd.Title = model.Title;
                educationToAdd.UniversityName = model.UniversityName;
                educationToAdd.Description = model.Description;
                educationToAdd.Adress = model.Adress;
                educationToAdd.DateBetween = model.DateBetween;

                educationToAdd = await _educationDal.CreateAsync(educationToAdd);
                valid = true;
            }

            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors,
                Education = educationToAdd
            });
        }

        [HttpPost]
        public async Task<JsonResult> EditEducation(EditEducationViewModel model)
        {
            List<string> allErrors = new List<string>();
            Education eduCallback = new Education();
            bool valid = false;
            if (!ModelState.IsValid)
            {
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                var skillToUpdateR = await _educationDal.GetById(model.EducationId);

                skillToUpdateR.Title = model.Title;
                skillToUpdateR.Adress = model.Adress;
                skillToUpdateR.DateBetween = model.DateBetween;
                skillToUpdateR.UniversityName= model.UniversityName;
                skillToUpdateR.Description = model.Description;

                eduCallback =await _educationDal.ReturnUpdateAsync(skillToUpdateR);
                valid = true;
            }
            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors,
                Education = eduCallback
            });

        }

        [HttpPost]
        public async Task<JsonResult> DeleteEducation(int EduId)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;

            if (EduId == 0)
            {
                ModelState.AddModelError("", "Error");
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                await _educationDal.DeleteAsync(await _educationDal.GetById(EduId));
                valid = true;
            }
            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors
            });
        }


        //-------------experience--------

        public IActionResult Experiences()
        {
            //test education degiscek
            ResumeViewModel model = new ResumeViewModel();
            model.Educations = _educationDal.GetAll().ToList();
            return View(model);

        }

        public IActionResult ExperienceEdit()
        {
            return View();
        }

    }
}
