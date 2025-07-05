using KAShop.Data;
using KAShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KAShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }

        // GET: Admin/Categories/Delete 
        public IActionResult Delete(int id)
        {
            var category = context.Categories.FirstOrDefault(c=> c.Id == id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                TempData["success"] = "Category deleted successfully.";
            }
            else
            {
                TempData["message"] = "Category not found.";
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Admin/Categories/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category request)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(request);
                context.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
