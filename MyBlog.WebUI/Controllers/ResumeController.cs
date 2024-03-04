using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Models.Resume;
using MyBlog.WebUI.Util.Abstract;
using System.Data;

namespace MyBlog.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ResumeController : Controller
    {
        private readonly IEducationDal _educationDal;
        private readonly IMethods _methods;
        private readonly IExperienceDal _experienceDal;
        public ResumeController(IEducationDal educationDal, IMethods methods, IExperienceDal experienceDal)
        {
            _educationDal = educationDal;
            _methods = methods;
            _experienceDal = experienceDal;
        }

        public async Task<IActionResult> Education()
        {
            ResumeViewModel model = new ResumeViewModel();
            model.Educations = _educationDal.GetAll().ToList();
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
                skillToUpdateR.UniversityName = model.UniversityName;
                skillToUpdateR.Description = model.Description;

                eduCallback = await _educationDal.ReturnUpdateAsync(skillToUpdateR);
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

        public async Task<IActionResult> Experiences()
        {

            var experiences = _experienceDal.GetAll().OrderBy(x=>x.DisplayOrder).Select(e =>
                new ExperienceViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    DateBetween = e.DateBetween,
                    CompanyName = e.CompanyName,
                    Adress = e.Adress,
                    ExperienceSteps = e.ExperienceSteps,
                    DisplayOrder= e.DisplayOrder,
                    

                }).ToList();

            return View(experiences);
        }

        public async Task<IActionResult> CreateExperience()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperience(ExperienceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Experience experience = new Experience
                {
                    Title = model.Title,
                    DateBetween = model.DateBetween,
                    CompanyName = model.CompanyName,
                    Adress = model.Adress,
                    ExperienceSteps = model.ExperienceSteps,
                    DisplayOrder=model.DisplayOrder
                };
                await _experienceDal.CreateAsync(experience);

                return RedirectToAction("Experiences");
            }

            return View(model);

        }

        public async Task<IActionResult> ExperienceEdit(int id)
        {
            if (id == 0)
                ModelState.AddModelError("", "Error ! Try Again");

            var experience = await _experienceDal.GetById(id);

            if (experience == null)
                ModelState.AddModelError("", "Error ! Try Again");
            else
            {
                return View(new ExperienceViewModel
                {
                    Id = experience.Id,
                    Title = experience.Title,
                    DateBetween = experience.DateBetween,
                    CompanyName = experience.CompanyName,
                    Adress = experience.Adress,
                    ExperienceSteps=experience.ExperienceSteps,
                    DisplayOrder= experience.DisplayOrder
                    
                });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExperienceEdit(ExperienceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var experience = await _experienceDal.GetById(model.Id);

                if (experience == null)
                {
                    ModelState.AddModelError("", "Error ! Try Again");
                    return View(experience);
                }
                else
                {
                    experience.Title = model.Title;
                    experience.DateBetween = model.DateBetween;
                    experience.Adress = model.Adress;
                    experience.CompanyName = model.CompanyName;
                    experience.Description = model.Description;
                    experience.ExperienceSteps=model.ExperienceSteps;
                    experience.DisplayOrder= model.DisplayOrder;

                    await _experienceDal.UpdateAsync(experience);

                    return RedirectToAction("Experiences");
                }


            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> DeleteExperience(int ExpId)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;

            if (ExpId == 0)
            {
                ModelState.AddModelError("", "Error");
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                await _experienceDal.DeleteAsync(await _experienceDal.GetById(ExpId));
                valid = true;
            }
            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors
            });
        }

    }
}
