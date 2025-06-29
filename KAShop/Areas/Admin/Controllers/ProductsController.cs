using KAShop.Data;
using KAShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KAShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        // GET: Admin/Categories
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        // GET: Admin/Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }
        // POST: Admin/Products/Create
        [HttpPost]
        public IActionResult Create(Product request, IFormFile Image)
        {
            //var fileName = Guid.NewGuid().ToString(); //432823498243234
            //fileName = fileName + Path.GetExtension(Image.FileName);
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
            //using (var stream = System.IO.File.Create(filePath))
            //{
            //    Image.CopyTo(stream);
            //}
            var imageService = new Services.ImageService();
            string fileName = imageService.UploadImage(Image);
            request.Image = fileName;
            context.Products.Add(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete: Admin/Products/Delete/5
       
        public IActionResult Delete(int id) {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            var imageService = new Services.ImageService();
            imageService.DeleteImage(product.Image);


            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
