
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Entity;
using MyBlog.WebUI.Util;
using MyBlog.WebUI.Util.Abstract;
using Smidge;


var builder = WebApplication.CreateBuilder(args);

//smidge
builder.Services.AddSmidge(builder.Configuration.GetSection("smidge"));

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
    options.UseSqlServer(connectionString);
});


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

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

// auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

//layouts bundle
app.UseSmidge(bundle =>
{

    //home-css
    bundle.CreateCss("my-css-bundle",
        "~/Personal/assets/vendor/bootstrap/css/bootstrap.min.css",
        "~/Personal/assets/vendor/bootstrap-icons/bootstrap-icons.css",
        "~/Personal/assets/vendor/boxicons/css/boxicons.min.css",
        "~/Personal/assets/vendor/glightbox/css/glightbox.min.css",
        "~/Personal/assets/vendor/remixicon/remixicon.css",
        "~/Personal/assets/vendor/swiper/swiper-bundle.min.css",
        "~/lib/sweetalert2/sweetalert2.min.css",
        "~/Personal/assets/css/style.css",
        "~/css/site.css");

    //home-js
    bundle.CreateJs("my-js-bundle",
        "~/lib/jquery/jquery.min.js",
        "~/Personal/assets/vendor/purecounter/purecounter_vanilla.js",
        "~/Personal/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
        "~/Personal/assets/vendor/glightbox/js/glightbox.min.js",
        "~/Personal/assets/vendor/isotope-layout/isotope.pkgd.min.js",
        "~/Personal/assets/vendor/swiper/swiper-bundle.min.js",
        "~/Personal/assets/vendor/waypoints/noframework.waypoints.js",
        "~/lib/sweetalert2/sweetalert2.min.js",
        "~/js/sweetAlertCustom.js",

        "~/Personal/assets/js/main.js",
        "~/js/snowstorm.js",
        "~/js/olum-writer.js"
        );

    //admin-css
    bundle.CreateCss("my-admin-css-bundle",
        "~/admin/css/bootstrap-reboot.min.css",
        "~/admin/css/bootstrap-grid.min.css",
        "~/admin/css/magnific-popup.css",
        "~/admin/css/jquery.mCustomScrollbar.min.css",
        "~/admin/css/select2.min.css",
        "~/admin/css/ionicons.min.css",
        "~/admin/css/admin.css",
        "~/lib/sweetalert2/sweetalert2.min.css"

        );

    //admin-js
    bundle.CreateJs("my-admin-js-bundle",
        "~/admin/js/jquery-3.5.1.min.js",
        "~/admin/js/bootstrap.bundle.min.js",
        "~/admin/js/jquery.magnific-popup.min.js",
        "~/admin/js/jquery.mousewheel.min.js",
        "~/admin/js/jquery.mCustomScrollbar.min.js",
        "~/admin/js/select2.min.js",
        "~/admin/js/admin.js",
        "~/lib/sweetalert2/sweetalert2.min.js",
        "~/js/sweetAlertCustom.js"

        );

    //cusom-pages
    bundle.CreateJs("about-edit", "~/js/About/Edit/Edit.js");
    bundle.CreateJs("contact-index", "~/js/Contact/index.js");
    bundle.CreateJs("contact-contact", "~/js/Contact/Contact.js");

    bundle.CreateJs("portfolio-create", "~/js/Portfolio/Create/CreatePortfolio.js");
    bundle.CreateJs("portfolio-edit", "~/js/Portfolio/Edit/EditProject.js");
    bundle.CreateJs("portfolio-index", "~/js/Portfolio/Index/index.js");

    bundle.CreateJs("resume-education", "~/js/Resume/Education/Education.js");
    bundle.CreateJs("resume-experience", "~/js/Resume/Experience/Experiences.js");

});

app.Run();
