using CoffeeWebApp.Data;
using CoffeeWebApp.Helpers;
using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;
using CoffeeWebApp.Repository;
using CoffeeWebApp.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


   
var builder = WebApplication.CreateBuilder(args);

        
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloundinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("CoffeeAppDatabase"));
});

builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Truy c?p IdentityOptions
builder.Services.Configure<IdentityOptions>(options => {
    // Thi?t l?p v? Password
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequiredLength = 8; 
    options.Password.RequiredUniqueChars = 0;
    // C?u hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; 
    options.Lockout.AllowedForNewUsers = true;

    // C?u hình v? User.
    options.User.AllowedUserNameCharacters = // các ký t? ??t tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; // Email là duy nh?t

    // C?u hình ??ng nh?p.
    options.SignIn.RequireConfirmedEmail = false; // C?u hình xác th?c ??a ch? email (email ph?i t?n t?i)
    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác th?c s? ?i?n tho?i

});

// C?u hình Cookie
builder.Services.ConfigureApplicationCookie(options => {
    // options.Cookie.HttpOnly = true;  
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = $"/login/";                                 // Url ??n trang ??ng nh?p
    options.LogoutPath = $"/logout/";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";   // Trang khi User b? c?m truy c?p
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // Trên 5 giây truy c?p l?i s? n?p l?i thông tin User (Role)
    // SecurityStamp trong b?ng User ??i -> n?p l?i thông tinn Security
    options.ValidationInterval = TimeSpan.FromSeconds(5);
});
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddHttpClient();
var app = builder.Build();



if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}
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
app.UseSession();

app.MapControllerRoute(
    name: "adminArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
 