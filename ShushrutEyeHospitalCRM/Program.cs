using Microsoft.AspNetCore.Authentication.Cookies;
using Rotativa.AspNetCore;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapper();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = CommonHelper.LoginUrl;
           options.LogoutPath = CommonHelper.LoginUrl;
           options.AccessDeniedPath = CommonHelper.AccessDeniedUrl;
           options.ExpireTimeSpan = TimeSpan.FromDays(7);
           options.Cookie.Expiration = TimeSpan.FromDays(7);
       });
builder.Services.ConfigureApplicationCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
