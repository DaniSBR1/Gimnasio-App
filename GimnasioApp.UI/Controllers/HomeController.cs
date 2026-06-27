using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GimnasioApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Membresias");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        }
    }
}
