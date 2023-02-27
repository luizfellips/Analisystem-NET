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
		private readonly IEmail _email;
        public LoginController(IUserRepository userRepository, IUserSession userSession, IEmail email)
        {
            _userRepository = userRepository;
            _userSession = userSession;
            _email = email;
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
		public IActionResult RedefinePassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SendNewPassword(RedefinePasswordModel redefinePasswordModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UserModel user = _userRepository.getUserByLoginAndEmail(redefinePasswordModel.Login, redefinePasswordModel.Email);
					if(user != null)
					{
						string newPassword = user.GenerateNewPassword();
						string message = $"Your new password is: {newPassword}";
						bool sentMail = _email.Send(user.Email, "New password", message);
						if (sentMail)
						{
							_userRepository.updateUser(user);
							TempData["SuccessMessage"] = "We sent an email with your new password.";
						}
						else
						{
                            TempData["ErrorMessage"] = "We could not send an email. Please try again.";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["ErrorMessage"] = "We couldn't redefine your password, verify your credentials and try again.";
                    return RedirectToAction("RedefinePassword", "Login");
                }
				return View("Index");
			}
			catch (Exception error)
			{
                TempData["ErrorMessage"] = $"Oops! We couldn't update your password. More details on the exception: {error.Message}";
                return RedirectToAction("Index", "Login");
            }
		}
    }
}
