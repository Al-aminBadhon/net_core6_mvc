using App.BLL.ServiceContracts;
using App.BLL.Services;
using App.DAL.Data;
using App.DAL.Models;
using App.DAL.Repositories;
using App.DAL.RepositoryContracts;
using App.Home.FileUploadService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MHDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));


builder.Services.AddTransient<IDirectorsService, DirectorsService>();
builder.Services.AddTransient<IDirectorsRepository, DirectorsRepository>();

builder.Services.AddTransient<IGalleryService, GalleryService>();
builder.Services.AddTransient<IGalleryRepository, GalleryRepository>();

builder.Services.AddTransient<IFileUploadService, FileUploadService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<App.DAL.Utilities.AppUser>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.LoginPath = "/Login/Login";
                    options.AccessDeniedPath = "/Login/Login";
                }
                );
builder.Services.AddSession(
    obj =>
    {
        obj.IdleTimeout = TimeSpan.FromMinutes(10);
        obj.Cookie.HttpOnly = true;
        obj.Cookie.IsEssential = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
//    RequestPath = "/Scripts"
//});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
