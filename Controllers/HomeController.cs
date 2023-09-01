using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
			this.context = context;

		}

        [Authorize]
        public IActionResult Index()
        {
            var product = context.products.ToList();
			return View(product);
        }

        public IActionResult AddProduct(Product product)
        {
            context.products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateNewProduct(Product product)
        {
			context.products.Add(product);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult ProductDetails(int id)
		{
			var ProductDetails = context.productDetails.Where(p => p.ProductId == id).ToList();
			var product = context.products.ToList();
			ViewBag.ProductDetails = ProductDetails;
			return View(product);
		}

		public IActionResult ProductDetails()
        {
			var product = context.products.ToList();
            var ProductDetails = context.productDetails.ToList();
            ViewBag.ProductDetails = ProductDetails;
			return View(product);
		}

		public IActionResult Delete(int id)
        {
            var product = context.products.SingleOrDefault(x=> x.Id == id);
            if (product != null)
            {
                context.products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = context.products.SingleOrDefault(x => x.Id == id);
            
            return View(product);
        }

        public IActionResult AddProductDetails(ProductDetails productDetail)
        {
			context.productDetails.Add(productDetail);
			context.SaveChanges();
			return RedirectToAction("ProductDetails");
		}

		public IActionResult UpdateProduct(Product product)
        {
            Product productupdate = context.products.SingleOrDefault(x=> x.Id == product.Id)?? new Product();
            if (productupdate != null)
            {
                productupdate.ProductName = product.ProductName;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}