using Microsoft.AspNetCore.Mvc;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    public class AccountController(AppDbContext context) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.Users.FirstOrDefault(
                x => x.Username == username &&
                     x.Password == password
            );

            if (user != null)
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }

            ViewBag.Error = "Invalid username or password.";

            return View();
        }
    }
}