using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}