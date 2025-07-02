using KAShop.Data;
using KAShop.Models;
using KAShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;


namespace KAShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        // GET: Admin/Categories
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.Include(p => p.Category).ToList();
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
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = context.Categories.ToList();
                return View(request);
            }
            var imageService = new Services.ImageService();
            string fileName = imageService.UploadImage(Image);
            request.Image = fileName;
            context.Products.Add(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/Products/Edit/5

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = context.Categories.ToList();
            var product = context.Products.Find( id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }
        // POST: Admin/Products/Edit/5
        [HttpPost]
        public IActionResult Edit(Product request, IFormFile? Image)
        {
            var exitingProduct = context.Products.AsNoTracking().FirstOrDefault(p => p.Id == request.Id);
            if (Image != null)
            {
                var imageService = new ImageService();
                string fileName = imageService.UploadImage(Image);
                request.Image = fileName;
                imageService.DeleteImage(exitingProduct.Image);
            }
            else
            {
                request.Image = exitingProduct.Image;
            }
            context.Products.Update(request);
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
