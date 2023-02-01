using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
//    AddCookie(options => { options.LoginPath = "/Admin/login"; });
//builder.Services.AddControllers(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                     .RequireAuthenticatedUser()
//                     .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});

builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000
});
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
app.UseAuthentication();//eklediðimiz
app.UseAuthorization();//varsayýlan gelen



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "musteri",
    pattern: "Musteri/listele",
    defaults: new { controller = "Musteri", action = "Index" });

    endpoints.MapControllerRoute(name: "menu",
   pattern: "Menu/listele",
   defaults: new { controller = "Menu", action = "Index" });


    endpoints.MapControllerRoute(name: "oyun",
   pattern: "Oyun/listele",
   defaults: new { controller = "Oyun", action = "Index" });


    endpoints.MapControllerRoute(name: "oyunkadrosu",
   pattern: "OyunKadrosu/listele",
   defaults: new { controller = "OyunKadrosu", action = "Index" });


    endpoints.MapControllerRoute(name: "oyunsalon",
   pattern: "Seans/listele",
   defaults: new { controller = "OyunSalon", action = "Index" });


    endpoints.MapControllerRoute(name: "oyunsalonmusteri",
   pattern: "Bilet/listele",
   defaults: new { controller = "OyunSalonMusteri", action = "Index" });


    endpoints.MapControllerRoute(name: "salon",
   pattern: "Salon/listele",
   defaults: new { controller = "Salon", action = "Index" });


    endpoints.MapControllerRoute(name: "tur",
   pattern: "Tur/listele",
   defaults: new { controller = "Tur", action = "Index" });

    endpoints.MapControllerRoute(name: "tiyatroslider",
   pattern: "TiyatroSlider/listele",
   defaults: new { controller = "TiyatroSlider", action = "Index" });

});

app.Run();
