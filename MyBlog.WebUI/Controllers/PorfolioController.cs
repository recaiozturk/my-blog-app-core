using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.Util;

namespace MyBlog.WebUI.Controllers
{
    public class PorfolioController : Controller
    {
        //private readonly TestContext testContext;

        //public PorfolioController(TestContext testContext)
        //{
        //    this.testContext = testContext;
        //}

        public async Task<IActionResult> PorfolioDetailAsync(int id)
        {
            //test
            //ViewBag.TestData = await testContext.Favorites.FirstOrDefaultAsync();

            var pd = TempProfolioList.FirstOrDefault(i => i.Id == id);
            if (pd == null)
                return RedirectToAction("/");
            return View(pd);
        }


        private static List<TempPorrtfolioDetailModel> TempProfolioList = new List<TempPorrtfolioDetailModel>()
        {
            new TempPorrtfolioDetailModel
            {
                Id=1,
                Name="Dota 2 Caht Wheel Sounds",
                AppType=(int)Enums.AppType.Mobil,
                TempCreatedDate=new DateTime(2011, 6, 10).ToString(),
                ProjectUrl="https://github.com/recaiozturk/dota2-chatwheel-sounds-mobileapp",
                ProjectDetail="I developed an application where dota enthusiasts can listen to dota chat wheel sounds.",
                UsedTechnologies="Unity3D,c#,gimp",
                Image1="dota1.png",
                Image2="dota2.png",
                Image3="dota3.png"
            }
        };

    }
}