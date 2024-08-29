using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Util;
using MyBlog.WebUI.Util.Abstract;
using System.Data;
using System.Runtime.InteropServices;

namespace MyBlog.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AboutController : Controller
    {
        //test code  2 3
        private readonly IAboutDal _aboutRepository;
        private readonly IMethods _methods;
        private readonly IMapper _mapper;
        public AboutController(IAboutDal aboutRepository, IMethods methods, IMapper mapper)
        {
            _aboutRepository = aboutRepository;
            _methods = methods;
            _mapper = mapper;
        }

        public async Task<IActionResult>Edit()
        {
            
            var about  = await _aboutRepository.GetAboutAsync();
            var viewModel= _mapper.Map<AboutViewModel>(about);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutViewModel model, IFormFile imageFile)
        {
            var oldData = await _aboutRepository.GetAboutAsync();
            model.Skills= oldData.Skills;
            ModelState.Remove("imageFile");

            if (imageFile == null)
            {

                model.Image = oldData.Image;
            }
            else
            {
                var imageFileModel = _methods.CreateImageFileAsync(imageFile, (int)Enums.ImageType.Profile);
                model.Image = imageFileModel.Result.ImageCreatedName;
            }
            
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return NotFound();

                var aboutToUpdate = _mapper.Map<About>(model);
                aboutToUpdate.Skills = oldData.Skills;
                await _aboutRepository.UpdateAboutAsync(aboutToUpdate);

                var viewModel = _mapper.Map<AboutViewModel>(aboutToUpdate);

                return View(viewModel);
            }
            else
            {
                model.Skills = oldData.Skills;
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
                allErrors= _methods.ModelErrors(ModelState);
            }          
            else
            {
                skillToAdd.SkillName= skill.SkillName;
                skillToAdd.SkillValue = skill.SkillValue;
                skillToAdd= await _aboutRepository.AddSkillToAboutAsync(skillToAdd);
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
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                var skillToUpdateR = _aboutRepository.GetSkillByIdAsync(skill.SkillId);

                skillToUpdateR.Result.SkillName = skill.SkillName;
                skillToUpdateR.Result.SkillValue= skill.SkillValue;

                _aboutRepository.EditSkillForAboutAsync(skillToUpdateR.Result);
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
        public async Task<JsonResult> DeleteSkillForAboutAsync(int skillId)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;

            if (skillId == 0)
            {
                ModelState.AddModelError("", "Error Happened");
                allErrors = _methods.ModelErrors(ModelState);
            }
            else
            {
                await _aboutRepository.DeleteSkillForAboutAsync(skillId);
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
