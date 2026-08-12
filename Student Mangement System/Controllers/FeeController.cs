using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    public class FeeController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var fee = context.Fees
                      .Include(s => s.Student)
                      .ToList();
            return View(fee);
        }
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName"
            );

            return View();
        }
        [HttpPost]
        public IActionResult Create(Fee fee)
        {
            if (ModelState.IsValid)
            {
                context.Fees.Add(fee);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                fee.StudentId
            );

            return View(fee);
        }
        public IActionResult Edit(int id)
        {
            var fee = context.Fees.Find(id);

            if (fee == null)
            {
                return NotFound();
            }

            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                fee.StudentId
            );

            return View(fee);
        }
        [HttpPost]
        public IActionResult Edit(Fee fee)
        {
            if (ModelState.IsValid)
            {
                context.Fees.Update(fee);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                fee.StudentId
            );

            return View(fee);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var fee = context.Fees.Find(id);

            if (fee == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Fee record not found."
                });
            }

            context.Fees.Remove(fee);
            context.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Fee record removed successfully."
            });
        }
    }
}
