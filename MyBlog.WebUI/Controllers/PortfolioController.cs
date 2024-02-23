using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Models.Portfolio;
using MyBlog.WebUI.Util;
using System;

namespace MyBlog.WebUI.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioDal _portfolioDal;

        public PortfolioController(IPortfolioDal portfolioDal)
        {
            _portfolioDal = portfolioDal;
        }

        public async Task<IActionResult> Index()
        {
            var portfolios = await _portfolioDal.GetPortfolios();

            if(portfolios==null)
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

            //List<int> enumValuesList = Enum.GetValues(typeof(Enums.AppType))
            //                           .Cast<int>()
            //                           .ToList();


            if (ModelState.IsValid)
            {
                Portfolio portfolio= new Portfolio
                {
                    Title = model.Title,
                    ProjectDate = model.ProjectDate,
                    PortfolioType = model.PortfolioType,
                    Description = model.Description,
                    ProjectUrl = model.ProjectUrl,
                    UsedTechnologies = model.UsedTechnologies,
                };

                await _portfolioDal.CreateAsync(portfolio);

                return RedirectToAction("EditProject", new { id = portfolio.Id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddImageForPortfolio(IFormFile ImageFile,int PortfolioId,bool IsDefault)
        {
            //parametreler düzgün geliyor ,resim ekleme işlemi yapılcak
            return Json("");
        }
        public async Task<IActionResult> EditProject(int id)
        {
            if(id == 0) return NotFound();

            var portfolio= await _portfolioDal.GetById(id);


            return View(new PortfolioViewModel {
                
                Id = portfolio.Id,
                Title = portfolio.Title,
                ProjectDate = portfolio.ProjectDate,
                Description = portfolio.Description,
                ProjectUrl= portfolio.ProjectUrl,
                PortfolioType= portfolio.PortfolioType,
                UsedTechnologies= portfolio.UsedTechnologies
            
            });
        }


        public IActionResult Detail()
        {
            return View();
        }
    }
}
