
using MyBlog.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSmidgeExt(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddRepServiceExt(builder.Configuration);
builder.Services.AddEmailExt(builder.Configuration);

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

app.UseDeveloperExceptionPage();

app.UseSmidgeExt();

app.Run();
