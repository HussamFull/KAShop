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

        // GET: Admin/Categories/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                TempData["message"] = "Category not found.";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        // POST: Admin/Categories/Edit
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(Category request)
        {
            if (ModelState.IsValid)
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == request.Id);
                if (category != null)
                {
                    category.Name = request.Name;
                    context.SaveChanges();
                    TempData["success"] = "Category updated successfully.";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Category not found.";
            }
            return View(request);

        }
    }
}
