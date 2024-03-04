using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IForumRepository, ForumRepository>();
builder.Services.AddScoped<IThreadRepository, ThreadRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddIdentity<User, IdentityRole>(options => { })
        .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
builder.Services.AddCoreAdmin();

var app = builder.Build();

await Seed.SeedUsersAndRolesAsync(app);

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");



using (var serviceScope = app.Services.CreateScope())
{
    //Roles
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    if (!await roleManager.RoleExistsAsync(UserRoles.AppUser))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.AppUser));

    //Users
    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string adminUserEmail = "admin@admin.com";

    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
    if (adminUser == null)
    {
        var newAdminUser = new User()
        {
            UserName = "admin",
            Email = adminUserEmail,
            EmailConfirmed = true,
        };
        await userManager.CreateAsync(newAdminUser, "zaq1@WSX");
        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
    }

    string appUserEmail = "user@etickets.com";

    var appUser = await userManager.FindByEmailAsync(appUserEmail);
    if (appUser == null)
    {
        var newAppUser = new User()
        {
            UserName = "app-user",
            Email = appUserEmail,
            EmailConfirmed = true,
        };
        await userManager.CreateAsync(newAppUser, "Coding@1234?");
        await userManager.AddToRoleAsync(newAppUser, UserRoles.AppUser);
    }
}

app.MapDefaultControllerRoute();
app.Run();
