using Infrastructure.DBContextconnection;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContextClass>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireDigit = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
}).AddEntityFrameworkStores
<DbContextClass>().AddDefaultTokenProviders().AddUserStore<UserStore<ApplicationUser, ApplicationRole, DbContextClass, int>>().
AddRoleStore<RoleStore<ApplicationRole, DbContextClass, int>>();

builder.Services.AddAuthorization(option =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.FallbackPolicy = policy;
    //option.AddPolicy("Auth", option2 =>
    //{
    //    option2.RequireAssertion(option =>
    //    {
    //        return !option.User.Identity.IsAuthenticated;
    //    });
    //});
    
});
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/register";
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
