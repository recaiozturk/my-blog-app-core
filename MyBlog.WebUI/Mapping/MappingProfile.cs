using AutoMapper;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;
using MyBlog.WebUI.Models.Contact;
using MyBlog.WebUI.Models.Portfolio;

namespace MyBlog.WebUI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<About, AboutViewModel>().ReverseMap();
            CreateMap<Portfolio, PortfolioViewModel>().ReverseMap();
            CreateMap<Contact, ContactFormViewModel>().ReverseMap();
        }
    }
}
