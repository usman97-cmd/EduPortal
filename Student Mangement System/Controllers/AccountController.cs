using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.Services;
using Student_Mangement_System.ViewModels;
using System.Security.Claims;

namespace Student_Mangement_System.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
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
            var result =  accountService.Register(vm);
            if (!result)
            {
                ModelState.AddModelError("Username", "Username  or Email already exists");   
                return View(vm);
            }

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
            var user = accountService.Login(vm);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(vm);
            }
           
            // Step 1 Claims 
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("username",user.Username),
            new Claim (ClaimTypes.Role,user.Role)
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
            if (user.Role == "Admin") {
                return RedirectToAction("Index", "Dashboard");
            }
           return RedirectToAction("Index","Profile" );
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }
    }
}