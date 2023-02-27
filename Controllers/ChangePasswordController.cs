using Analisystem.Helper;
using Analisystem.Models;
using Analisystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Analisystem.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public ChangePasswordController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Change(ChangePasswordModel changePasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel loggedUser = _userSession.GetUserSession();
                    changePasswordModel.Id = loggedUser.Id;
                    _userRepository.changePassword(changePasswordModel);
                    TempData["SuccessMessage"] = "Password changed successfully.";
                    return View("Index", changePasswordModel);
                }
                return View("Index", changePasswordModel);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Could not change your password. more details: {error.Message}";
                return View("Index", changePasswordModel);
            }
        }

    }
}
