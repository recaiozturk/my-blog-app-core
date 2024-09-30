using Microsoft.AspNetCore.Identity;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Util.Abstract;
using MyBlog.WebUI.Util;
using Smidge;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.WebUI.Extensions
{
    public static class RepoAndServiceExtensions
    {
        public static IServiceCollection AddRepServiceExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                var connectionString = configuration.GetConnectionString("database");
                options.UseSqlServer(connectionString);
            });


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();   
            
            services.AddScoped<IAboutDal, EfAboutDal>();
            services.AddScoped<IEducationDal, EfEducationDal>();
            services.AddScoped<IExperienceDal, EfExperienceDal>();
            services.AddScoped<IPortfolioDal, EfPortfolioDal>();
            services.AddScoped<IProjectImageDal, EfProjectImageDal>();
            services.AddScoped<IContactDal, EfContactDal>();
            services.AddScoped<IMethods, Methods>();

            // auto mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
