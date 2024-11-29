using Microsoft.AspNetCore.Mvc;

namespace Translator_Webapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
