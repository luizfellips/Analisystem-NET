using Analisystem.Filters;
using Analisystem.Helper;
using Analisystem.Models;
using Analisystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Analisystem.Controllers
{
    [LoggedFilter]


    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        private readonly IProductRepository _productRepository;

        public UserController(IUserRepository userRepository, IUserSession userSession, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _userSession = userSession;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            UserModel user = _userSession.GetUserSession();
            return View(user);
        }
        [AdminFilter]
        public IActionResult Management()
        {
            List<UserModel> users = _userRepository.getUsers();
            return View(users);
        }
        [AdminFilter]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [AdminFilter]
        public IActionResult CreateUser(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(user.CPF != null)
                    {
                        int firstDigit = CpfValidator.getFirstDigit(user.CPF);
                        user.CPF += firstDigit.ToString();
                        int secondDigit = CpfValidator.getSecondDigit(user.CPF);
                        user.CPF += secondDigit.ToString();
                        user.CPF = CpfValidator.CpfFormatter(user.CPF);
                    }
                    _userRepository.addUser(user);
                    TempData["SuccessMessage"] = "User added successfully!";
                    return RedirectToAction("Management");
                }
                return View(user);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Oops! We couldn't register your user. More details of the exception: {ex.Message}";
                return RedirectToAction("Management");
            }
        }
        [AdminFilter]
        public IActionResult Edit(int id)
        {
            UserModel user = _userRepository.getUserByID(id);
            return View(user);
        }

        [HttpPost]
        [AdminFilter]
        public IActionResult EditUser(UserNoPasswordModel userWithoutPassword)
        {
            try
            {
                UserModel user = null;
                if(ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        Id = userWithoutPassword.Id,
                        Name = userWithoutPassword.Name,
                        Login = userWithoutPassword.Login,
                        Email = userWithoutPassword.Email,
                        ProfileLevel = userWithoutPassword.ProfileLevel,
                        CPF = userWithoutPassword.CPF,
                        Phone = userWithoutPassword.Phone,
                        Address = userWithoutPassword.Address,
                    };
                    if (user.CPF != null)
                    {
                        int firstDigit = CpfValidator.getFirstDigit(user.CPF);
                        user.CPF += firstDigit.ToString();
                        int secondDigit = CpfValidator.getSecondDigit(user.CPF);
                        user.CPF += secondDigit.ToString();
                        user.CPF = CpfValidator.CpfFormatter(user.CPF);
                    }
                    _userRepository.updateUser(user);
                    TempData["SuccessMessage"] = "User updated successfully!";
                    return RedirectToAction("Management");
                }
                return View("Edit",user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Oops! We couldn't update your user. More details of the exception: {ex.Message}";
                return RedirectToAction("Management");
            }
        }
        [AdminFilter]
        public IActionResult ConfirmDelete(int id)
        {
			UserModel user = _userRepository.getUserByID(id);
			return View(user);
        }
        [AdminFilter]
        public IActionResult Delete(int id)
        {
			UserModel user = _userRepository.getUserByID(id);
            try
            {
				bool successfullyRemoved = _userRepository.removeUser(user);
				if (successfullyRemoved)
				{
					TempData["SuccessMessage"] = "User deleted successfully!";
					return RedirectToAction("Management");
				}
				else
				{
					TempData["ErrorMessage"] = "Oops! we could not delete your user.";
				}
				return View("Management");
			}
            catch(Exception error)
            {
				TempData["ErrorMessage"] = $"We could not delete your user. more details on the exception: {error.Message}";
                return View(user);
			}
			
		}

        public IActionResult UserProducts(int id)
        {
            List<ProductModel> products = _productRepository.getUserProducts(id);
            return PartialView("_UserProducts", products);
        }
    }
}
