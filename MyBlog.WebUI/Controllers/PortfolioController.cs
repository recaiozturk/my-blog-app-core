﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models.Portfolio;
using MyBlog.WebUI.Util;
using MyBlog.WebUI.Util.Abstract;
using System.Data;

namespace MyBlog.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioDal _portfolioDal;
        private readonly IMethods _methods;
        private readonly IProjectImageDal _projectImageDal;
        private readonly IMapper _mapper;

        public PortfolioController(IPortfolioDal portfolioDal, IMethods methods, IProjectImageDal projectImageDal, IMapper mapper)
        {
            _portfolioDal = portfolioDal;
            _methods = methods;
            _projectImageDal = projectImageDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()//admin Index
        {
            var portfolios = await _portfolioDal.GetPortfolios();

            if (portfolios == null)
                return NotFound();

            return View(portfolios);
        }

        public async Task<IActionResult> CreateProject()
        {
            return View(new PortfolioViewModel { });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(PortfolioViewModel model /*int[] appTypeIds*/)
        {
            List<ProjectImage> images = new List<ProjectImage>();

            if (ModelState.IsValid)
            {
                var portfolio=_mapper.Map<Portfolio>(model);

                await _portfolioDal.CreateAsync(portfolio);

                return RedirectToAction("EditProject", new { id = portfolio.Id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddImageForPortfolio(IFormFile ImageFile, int PortfolioId, bool IsDefault)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;
            ProjectImage image = new ProjectImage();

            if (PortfolioId == 0)
            {
                allErrors.Add("Portfolio is not found");

                return Json(new
                {
                    IsValid = valid,
                    ErrorMessages = allErrors
                });
            }

            var imageFileModel = await _methods.CreateImageFileAsync(ImageFile, (int)Enums.ImageType.Portfolio);
            if (!imageFileModel.IsValid)
                allErrors.Add(imageFileModel.ErrorString);
            else
            {
                image.PortfolioId = PortfolioId;
                image.ImageUrl = imageFileModel.ImageCreatedName;

                if (IsDefault)
                {
                    await _portfolioDal.ResetPortfolioImagesIsCover(PortfolioId);
                    image.IsCoverImage = true;
                }

                await _projectImageDal.CreateAsync(image);

                valid = true;

                return Json(new
                {
                    IsValid = valid,
                    ErrorMessages = allErrors,
                });

            }

            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors
            });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteImageFromPortfolio(int ImageId)
        {
            bool isValid = false;
            try
            {
                string[] fileNames = new string[1];
                var portImage = await _projectImageDal.GetById(ImageId);
                await _projectImageDal.DeleteAsync(portImage);
                fileNames[0] = portImage.ImageUrl;
                await _methods.DeletePortfolioImage(fileNames);
                isValid = true;
            }
            catch (Exception)
            {

            }

            return Json(new
            {
                IsValid = isValid,
                ErrorMessage = "Error !"
            });
        }


        [HttpPost]
        public async Task<JsonResult> DeletePortfolio(int PortfolioId)
        {
            bool valid = false;

            try
            {
                if (PortfolioId != 0)
                {
                    var portf = await _portfolioDal.GetPortfolioById(PortfolioId);
                    await _portfolioDal.DeletePortfolioWithImages(portf.Id);
                    await _methods.DeletePortfolioImage(portf.ProjectImages.Select(x => x.ImageUrl).ToArray());
                    valid = true;
                }
            }
            catch (Exception)
            {
                //log error
            }

            return Json(new
            {
                IsValid = valid,
                ErrorMessage = "Error !!",
            });
        }

        public async Task<IActionResult> EditProject(int id)
        {
            if (id == 0) return NotFound();

            var portfolio = await _portfolioDal.GetPortfolioById(id);

            var viewModel = _mapper.Map<PortfolioViewModel>(portfolio);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var portfolio = await _portfolioDal.GetPortfolioById(model.Id);
                var oldImages = portfolio.ProjectImages;



                if (portfolio == null) return NotFound();

                //_mapper.Map<Portfolio>(model);
                _mapper.Map(model, portfolio);
                portfolio.ProjectImages = oldImages;
                await _portfolioDal.UpdateAsync(portfolio);

            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> SetImageToCover(int ImageId,int PortfolioId)
        {
            bool isValid = false;
            string errorMessage = "";

            if (ImageId == 0 || PortfolioId == 0)
                errorMessage = "wrong match";
            try
            {
                await _portfolioDal.ResetPortfolioImagesIsCover(PortfolioId);
                var image = await _projectImageDal.GetById(ImageId);
                image.IsCoverImage = true;
                await _projectImageDal.UpdateAsync(image);
                isValid=true;

                return Json(new
                {
                    IsValid = isValid,
                    ErrorMessage = "",
                });
            }
            catch (Exception)
            {
                isValid = false;
                errorMessage = "Error Happened";
            }

            return Json(new
            {
                IsValid = isValid,
                ErrorMessage = errorMessage,
            });

        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var portfolio = await _portfolioDal.GetPortfolioById(id);

            if(portfolio == null)
                return NotFound();

            return View(portfolio);
        }
    }
}
