using AutoMapper;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.Mapping
{
    public class AboutProfile:Profile
    {
        public AboutProfile()
        {
            CreateMap<About, AboutViewModel>().ReverseMap();
        }
    }
}
