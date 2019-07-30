using System.Threading.Tasks;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Game;
using GameStore.Application.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameController : BaseController
    {
        public async Task<ViewResult> Index(int? p, string category = null)
        {
            var games = await Mediator.Send(new GetAllGamesQuery
            {
                Pagination = new PageRequestViewModel(p ?? 1, 5),
                Category = category
            });
            var categories = await Mediator.Send(new GetAllCategoriesQuery());

            return View(new GamesListViewModel
            {
                Games = games,
                Categories = categories
            });
        }
    }
}