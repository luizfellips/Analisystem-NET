using Analisystem.Helper;
using Analisystem.Models;
using Analisystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Analisystem.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserSession _userSession;
        public LoginController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
		{
			if (_userSession.GetUserSession() != null) return RedirectToAction("Index", "Home");
			return View();
		}

		public IActionResult ExitSession()
		{
			_userSession.RemoveUserSession();
			return RedirectToAction("Index", "Login");
		}
		[HttpPost]
        public IActionResult LoginResult(LoginModel loginUser)
        {
			try
			{
				if(ModelState.IsValid)
				{
					UserModel user = _userRepository.getUserByLogin(loginUser.Login);
					if(user != null)
					{
						if(user.ValidatePassword(loginUser.Password))
						{
							_userSession.CreateUserSession(user);
							return RedirectToAction("Index", "Home");
						}
                        TempData["ErrorMessage"] = "The password is incorrect, try again.";
                    }
                    TempData["ErrorMessage"] = "Login or password are invalid, please try again.";
                }
				return View("Index");
			}
			catch (Exception error)
			{
                TempData["ErrorMessage"] = $"Oops! We could not log you in. more details on the exception: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
