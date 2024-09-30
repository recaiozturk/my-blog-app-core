using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Util.Abstract;
using MyBlog.WebUI.Util;

namespace MyBlog.WebUI.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddEmailExt(this IServiceCollection services, IConfiguration configuration)
        {
            //mail settings
            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
            new SmtpEmailSender(
                configuration["EmailSender:Host"],
                configuration.GetValue<int>("EmailSender:Port"),
                configuration.GetValue<bool>("EmailSender:EnableSSL"),
                configuration["EmailSender:UserName"],
                configuration["EmailSender:Password"])
            );

            return services;
        }
    }
}
