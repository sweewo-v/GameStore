using System.Threading.Tasks;
using GameStore.Application.Queries.Category.GetAllCategories;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
