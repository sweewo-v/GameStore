using System.Linq;
using System.Threading.Tasks;
using GameStore.Application.Commands.Admin.SeedDatabase;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Admin;
using GameStore.Application.ViewModels.Pagination;
using GameStore.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(new AdminIndexViewModel
            {
                Games = (await Mediator.Send(new GetAllGamesQuery()
                {
                    Pagination = new PageRequestViewModel(1, 3)
                })).Items,
                Categories = (await Mediator.Send(new GetAllCategoriesQuery())).Take(3)
            });
        }

        public async Task<IActionResult> SeedDatabase()
        {
            await Mediator.Send(new SeedDatabaseCommand());

            return RedirectToAction(nameof(Index));
        }
    }
}
