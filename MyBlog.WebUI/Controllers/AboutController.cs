using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Util;

namespace MyBlog.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutDal _aboutRepository;       
        public AboutController(IAboutDal aboutRepository )
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<IActionResult>Edit()
        {
            
            var model  = await _aboutRepository.GetAboutAsync();
            return View(new AboutViewModel
            {
                AboutId = model.Id,
                Summary = model.Summary,
                Adress = model.Adress,
                Age = model.Age,
                Birthday = model.Birthday,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                Website = model.Website,
                Skills=model.Skills,
                FavoriteSerie=model.FavoriteSerie,
                FavoriteBook=model.FavoriteBook,
                FavoriteMovie=model.FavoriteMovie,
                FavoriteMusic=model.FavoriteMusic,
                
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.AboutId == null)
                    return NotFound();


                var aboutToUpdate = new About
                {
                    Id = model.AboutId,
                    Title = model.Title,
                    Adress=model.Adress,
                    Age=model.Age,
                    Birthday = model.Birthday,
                    Email = model.Email,
                    FavoriteBook = model.FavoriteBook,
                    FavoriteMovie = model.FavoriteMovie,
                    FavoriteMusic = model.FavoriteMusic,
                    FavoriteSerie = model.FavoriteSerie,
                    PhoneNumber=model.PhoneNumber,
                    Summary=model.Summary,
                    Website=model.Website
                };

                var testModel = await _aboutRepository.GetAboutAsync();
                aboutToUpdate.Skills = testModel.Skills;
                await _aboutRepository.UpdateAbout(aboutToUpdate);

                return View(new AboutViewModel
                {
                    AboutId = aboutToUpdate.Id,
                    Summary = aboutToUpdate.Summary,
                    Adress = aboutToUpdate.Adress,
                    Age = aboutToUpdate.Age,
                    Birthday = aboutToUpdate.Birthday,
                    Email = aboutToUpdate.Email,
                    PhoneNumber = aboutToUpdate.PhoneNumber,
                    Title = aboutToUpdate.Title,
                    Website = aboutToUpdate.Website,
                    Skills = aboutToUpdate.Skills,
                    FavoriteSerie = aboutToUpdate.FavoriteSerie,
                    FavoriteBook = aboutToUpdate.FavoriteBook,
                    FavoriteMovie = aboutToUpdate.FavoriteMovie,
                    FavoriteMusic = aboutToUpdate.FavoriteMusic,

                });
            }
            else
            {
                var entity = await _aboutRepository.GetAboutAsync();
                model.Skills = entity.Skills;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddSkillToAbout(SkillViewModel skill)
        {
            Skill skillToAdd = new Skill();
            List<string> allErrors = new List<string>();
            bool valid = false;
            if (!ModelState.IsValid)
            {
                Methods _methods = new();
                allErrors= _methods.ModelErrors(ModelState);
            }          
            else
            {
                skillToAdd.SkillName= skill.SkillName;
                skillToAdd.SkillValue = skill.SkillValue;
                skillToAdd= await _aboutRepository.AddSkillToAbout(skillToAdd);
                valid = true;
            }
                
            return Json(new
            {
                IsValid=valid,
                ErrorMessages= allErrors,
                Skill=skillToAdd
            });
        }

        [HttpPost]
        public JsonResult EditSkillForAbout(SkillViewModel skill)
        {
            List<string> allErrors = new List<string>();
            Skill skillToUpdate = new Skill();
            bool valid = false;
            if (!ModelState.IsValid)
            {
                Methods _methods = new();
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                var skillToUpdateR = _aboutRepository.GetSkillById(skill.SkillId);

                skillToUpdateR.Result.SkillName = skill.SkillName;
                skillToUpdateR.Result.SkillValue= skill.SkillValue;

                _aboutRepository.EditSkillForAbout(skillToUpdateR.Result);
                skillToUpdate=skillToUpdateR.Result;
                valid = true;
            }
            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors,
                Skill = skillToUpdate
            });
        }

        [HttpPost]
        public JsonResult DeleteSkillForAbout(int skillId)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;

            if (skillId == 0)
            {
                Methods _methods = new();
                ModelState.AddModelError("", "Error Happened");
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                _aboutRepository.DeleteSkillForAbout(skillId);
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
