using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using QLPreschool.Data;
using QLPreschool.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QlTMnContext>(options =>
{
    var connectionStr = builder.Configuration.GetConnectionString("SqlServer");
    options.UseSqlServer(connectionStr);
});

//Add Mail Service
var mailSetting = builder.Configuration.GetSection("MailSetting");

builder.Services.AddOptions();
builder.Services.Configure<MailSetting>(mailSetting);
builder.Services.AddTransient<IEmailSender, MailSenderService>();

//Add Identity Service
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<QlTMnContext>().AddDefaultTokenProviders();
// Truy c?p IdentityOptions
builder.Services.Configure<IdentityOptions>(options => {
    // Thi?t l?p v? Password
    options.Password.RequireDigit = false; // Kh�ng b?t ph?i c� s?
    options.Password.RequireLowercase = false; // Kh�ng b?t ph?i c� ch? th??ng
    options.Password.RequireNonAlphanumeric = false; // Kh�ng b?t k� t? ??c bi?t
    options.Password.RequireUppercase = false; // Kh�ng b?t bu?c ch? in
    options.Password.RequiredLength = 3; // S? k� t? t?i thi?u c?a password
    options.Password.RequiredUniqueChars = 1; // S? k� t? ri�ng bi?t

    // C?u h�nh Lockout - kh�a user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Kh�a 5 ph�t
    options.Lockout.MaxFailedAccessAttempts = 3; // Th?t b?i 5 l? th� kh�a
    options.Lockout.AllowedForNewUsers = true;

    // C?u h�nh v? User.
    options.User.AllowedUserNameCharacters = // c�c k� t? ??t t�n user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email l� duy nh?t

    // C?u h�nh ??ng nh?p.
    options.SignIn.RequireConfirmedEmail = false;            // C?u h�nh x�c th?c ??a ch? email (email ph?i t?n t?i)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // X�c th?c s? ?i?n tho?i
    options.SignIn.RequireConfirmedAccount = false;  //y�u c?u x�c th?c email r?i m?i cho ??ng nh?p

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
