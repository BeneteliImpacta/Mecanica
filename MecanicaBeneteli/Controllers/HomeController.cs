using Microsoft.AspNetCore.Mvc;

namespace MecanicaBeneteli.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var titulo = "Mecanica Beneteli";

            ViewData["Title"] = titulo;
            return View();
        }
    }
}
