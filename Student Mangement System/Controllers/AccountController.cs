using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.ViewModels;
using System.Security.Claims;

namespace Student_Mangement_System.Controllers
{
    public class AccountController(AppDbContext context) : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Username = vm.Username,
                Password = vm.Password,
            };          
                context.Users.Add(user);
                context.SaveChanges();

                return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = context.Users.FirstOrDefault(
                x => x.Username == vm.Username &&
                     x.Password == vm.Password
            );

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or Password");
                return View(vm);
            }
            // Step 1 Claims 
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("username",user.Username),
            };
            // step 2 claim Identity
            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            // step 3 ClaimsPrinciple
            var principle = new ClaimsPrincipal(identity);
            // step 4 CookieGenerate
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principle
            );
           return RedirectToAction("Index", "Dashboard");
        }
    }
}