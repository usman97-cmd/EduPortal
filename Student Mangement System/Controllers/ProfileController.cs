using Microsoft.AspNetCore.Mvc;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var profile = new ProfileViewModel
            {
                Name = "M. Usman",
                Email = "usmanshafique6517@gmail.com",
                Phone = "0329-4082935",
                Role = "Web Developer",
                ProfileImage = "/assets/images/Profile.jpeg"
            };

            return View(profile);
        }
    }
}