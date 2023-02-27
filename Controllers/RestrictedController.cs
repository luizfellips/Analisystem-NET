using Microsoft.AspNetCore.Mvc;

namespace Analisystem.Controllers
{
    public class RestrictedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
