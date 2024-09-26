using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
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
        private readonly IMapper _mapper;
        public ResumeController(IEducationDal educationDal, IMethods methods, IExperienceDal experienceDal, IMapper mapper)
        {
            _educationDal = educationDal;
            _methods = methods;
            _experienceDal = experienceDal;
            _mapper = mapper;
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
                educationToAdd=_mapper.Map<Education>(model);
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

                _mapper.Map(model, skillToUpdateR);

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
                var experience =_mapper.Map<Experience>(model);
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
                var experienceModel = _mapper.Map<ExperienceViewModel>(experience);
                return View(experienceModel);
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
                    _mapper.Map(model,experience);
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
