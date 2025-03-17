using BookingMovieCore.DTOs;
using Infrastructure.DBContextconnection;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingMovieUI.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly DbContextClass _dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(DbContextClass dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this._dbContext = dbContext;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            RegisterDTOcs resgisterDTO=new RegisterDTOcs();
            ViewBag.Country = new List<SelectListItem>()
            {
                new SelectListItem(){Text="India",Value="India"},
                 new SelectListItem(){Text="USA",Value="USA"},
                  new SelectListItem(){Text="UK",Value="UK"},
                   new SelectListItem(){Text="Germany",Value="Germany"},
                    new SelectListItem(){Text="Pakistan",Value="Pakistan"},
                     new SelectListItem(){Text="UAE",Value="UAE"},
                      new SelectListItem(){Text="OMAN",Value="OMAN"},
            };
            ViewBag.State = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Chandigarh",Value="Chandigarh"},
                 new SelectListItem(){Text="Delhi",Value="Delhi"},
                  new SelectListItem(){Text="Assam",Value="Assam"},
                   new SelectListItem(){Text="Goa",Value="Goa"},
                    new SelectListItem(){Text="Manipur",Value="Manipur"},
                     new SelectListItem(){Text="Haryana",Value="Haryana"},
                      new SelectListItem(){Text="Rajasthan",Value="Rajasthan"},
            };
            //ViewBag.City = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Chandigarh",Value="Chandigarh"},
            //     new SelectListItem(){Text="Delhi",Value="Delhi"},
            //      new SelectListItem(){Text="Dibrugarh",Value="Dibrugarh"},
            //       new SelectListItem(){Text="Noida",Value="Noida"},
            //        new SelectListItem(){Text="Muzzaffarpur",Value="Muzzaffarpur"},
            //         new SelectListItem(){Text="Patna",Value="Patna"}
            //};
            ViewBag.Role = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Admin",Value="Admin"},
                new SelectListItem(){Text="User",Value="User"}
            };

            return View(resgisterDTO);
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTOcs resgisterDTO)
        {
            ApplicationUser register = new ApplicationUser();
            register.UserName = resgisterDTO.Name;
            register.Email = resgisterDTO.Email;
            register.Address = resgisterDTO.Address;
            register.City = resgisterDTO.City;
            register.Zip = resgisterDTO.Zip;
            register.State = resgisterDTO.State;
            register.Country = resgisterDTO.Country;
            register.Role = resgisterDTO.Role;
            var user=await userManager.FindByNameAsync(resgisterDTO.Name);
            if (user == null)
            {
                var userCreated=await userManager.CreateAsync(register, resgisterDTO.Password);
                ApplicationRole role = new ApplicationRole()
                {
                    Name = resgisterDTO.Role
                };
                await roleManager.CreateAsync(role);
                await userManager.AddToRoleAsync(register, resgisterDTO.Role);
                if (userCreated.Succeeded)
                {
                    return RedirectToAction("login1");
                }
                else
                {
                    return RedirectToAction("LogOut");
                }
            }
            else
            {
                return RedirectToAction("LoginAllready");
            }
        }
        [Route("login1")]
        public IActionResult Login1()
        {
            return View();
        }
        [Route("login")]
        [HttpGet()]
        public IActionResult Login()
        {
            LoginDto login = new LoginDto();
            return View(login);
        }
        [Route("login")]
        [HttpPost()]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);
           
            if (user != null)
            {
                await signInManager.SignInAsync(user, false);

                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        public IActionResult LogOut()
        {
            return View();
        }
        public IActionResult LoginAllready()
        {
            return View();
        }
        [Route("Home")]
        [Authorize(Roles ="Admin")]
        public IActionResult Home()
        {
            return View();
        }

    }
}
