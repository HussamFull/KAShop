using KAShop.Data;
using Microsoft.AspNetCore.Mvc;


namespace KAShop.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {

             ViewBag.categories = context.Categories.ToList();
             ViewBag.products = context.Products.ToList();
            //var categories = context.Categories.ToList();
            return View();
            
        }
    }
}
