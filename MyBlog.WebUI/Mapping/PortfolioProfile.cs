using AutoMapper;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Models.Portfolio;

namespace MyBlog.WebUI.Mapping
{
    public class PortfolioProfile:Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioViewModel>().ReverseMap();
        }
    }
}
