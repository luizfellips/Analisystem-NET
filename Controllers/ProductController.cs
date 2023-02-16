using Analisystem.Models;
using Analisystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Analisystem.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
		{
			return View();
		}
        public IActionResult RegisterProduct()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Register(ProductModel product)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_productRepository.addProduct(product);
					TempData["SuccessMessage"] = $"Product {product.Name} registered successfully!";
					return RedirectToAction("Index");
				}
				return View(product);
			}
			catch (Exception error)
			{
				TempData["ErrorMessage"] = $"We could not register your product. more details on the exception: {error.Message}";
				return View(product);
			}
		}

        public IActionResult ManageProducts()
        {
			List<ProductModel> products = _productRepository.getProducts();
            return View(products);
        }
    }
}
