using Analisystem.Filters;
using Analisystem.Helper;
using Analisystem.Models;
using Analisystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Analisystem.Controllers
{
    [LoggedFilter]

    public class ProductController : Controller
	{
        private readonly IProductRepository _productRepository;
		private readonly IUserRepository _userRepository;
        private readonly IUserSession _session;

        public ProductController(IProductRepository productRepository, IUserRepository userRepository, IUserSession session)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _session = session;
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
					UserModel loggedUser = _session.GetUserSession();
					product.UserId = loggedUser.Id;
					_productRepository.addProduct(product);
					TempData["SuccessMessage"] = $"Product {product.Name} registered successfully!";
					return RedirectToAction("ManageProducts");
				}
				return View("RegisterProduct",product);
			}
			catch (Exception error)
			{
				TempData["ErrorMessage"] = $"We could not register your product. more details on the exception: {error.Message}";
                return View("RegisterProduct", product);
            }
		}

        public IActionResult ManageProducts()
        {
            UserModel loggedUser = _session.GetUserSession();
            if (loggedUser.ProfileLevel != Enums.AccessLevel.Admin)
            {
                List<ProductModel> products = _productRepository.getUserProducts(loggedUser.Id);
                return View(products);

            }
            else
			{
                List<ProductModel> products = _productRepository.getProducts();
                return View(products);
            }
        }

        public IActionResult EditProduct(int id)
        {
			ProductModel product = _productRepository.getProductById(id);
            return View(product);
        }
		[HttpPost]
		public IActionResult EditProduct(ProductModel product)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_productRepository.updateProduct(product);
					TempData["SuccessMessage"] = "We successfully updated your product informations!";
					return RedirectToAction("ManageProducts");
				}
				return View(product);
			}
			catch (Exception error)
			{
				TempData["ErrorMessage"] = $"We could not update your product. more details on the exception: {error.Message}";
				return View(product);
			}
		}
		
		public IActionResult ConfirmDelete(int id)
		{
			ProductModel product = _productRepository.getProductById(id);
			return View(product);
		}
		public IActionResult Delete(int id)
		{
			ProductModel product = _productRepository.getProductById(id);
			try
			{
				bool successfullyDeleted = _productRepository.removeProduct(product);
				if (successfullyDeleted)
				{
					TempData["SuccessMessage"] = $"Product {product.Name} successfully deleted!";
					return RedirectToAction("ManageProducts");
				}
				return View("ManageProducts");
			}
			catch (Exception error)
			{

				TempData["ErrorMessage"] = $"We could not delete the product. more details on the exception: {error.Message}";
				return View(product);
			}
		}
    }
}
