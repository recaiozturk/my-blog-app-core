using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Util;
using MyBlog.WebUI.Util.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//scoped  : her http isteðinde 1 nesne
builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IEducationDal, EfEducationDal>();
builder.Services.AddScoped<IExperienceDal, EfExperienceDal>();
builder.Services.AddScoped<IPortfolioDal, EfPortfolioDal>();
builder.Services.AddScoped<IProjectImageDal, EfProjectImageDal>();
builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IMethods, Methods>();

//mail settings
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"])
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//alýnan hatalarý gösterir(sunucuda)
app.UseDeveloperExceptionPage();


//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("X-Frame-Options", "ALLOW FROM https://www.youtube.com");
//    await next();
//});


app.Run();
