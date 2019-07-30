using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Application.Commands.Game.CreateGame;
using GameStore.Application.Commands.Game.DeleteGame;
using GameStore.Application.Commands.Game.UpdateGame;
using GameStore.Application.Exceptions;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.Queries.Game.GetGameById;
using GameStore.Application.Queries.GameCategory.GetAllGameCategoriesByGameId;
using GameStore.Application.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;
using GameStore.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class GameController : BaseController
    {
        public async Task<IActionResult> Index(int? p, string category = null)
        {
            var values = await Mediator.Send(new GetAllGamesQuery
            {
                Pagination = new PageRequestViewModel(p ?? 1, 5),
                Category = category
            });

            return View(values);
        }

        public async Task<ActionResult> Create()
        {
            return View(new CreateGameCommand
            {
                Name = "Game name",
                Description = "Game description",
                Price = 1,
                SelectedCategories = new List<int>(),
                AllCategories = await Mediator.Send(new GetAllCategoriesQuery())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]CreateGameCommand createGameCommand)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(createGameCommand);
                TempData["message"] =
                    $"Game \"{createGameCommand.Name}\" has been saved.";

                return RedirectToAction(nameof(Index));
            }

            return View(createGameCommand);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            try
            {
                var game = await Mediator.Send(new GetGameByIdQuery { Id = id.Value });
                var updateGameCommand = new UpdateGameCommand
                {
                    Name = game.Name,
                    Description = game.Description,
                    Id = game.Id,
                    Price = game.Price,
                    SelectedCategories = (await Mediator.Send(
                        new GetAllGameCategoriesByGameIdQuery { Id = game.Id }))
                        .Select(x => x.CategoryId).ToList(),
                    AllCategories = await Mediator.Send(new GetAllCategoriesQuery())
                };

                return View(updateGameCommand);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm]UpdateGameCommand updateGameCommand)
        {
            if (id != updateGameCommand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(updateGameCommand);
                    TempData["message"] =
                        $"Game \"{updateGameCommand.Name}\" has been updated.";

                    return RedirectToAction(nameof(Index));
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            try
            {
                var game = await Mediator.Send(new GetGameByIdQuery { Id = id.Value });

                return View(game);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteGameCommand { Id = id });
                TempData["message"] = $"Game with id \"{id}\" has been deleted.";

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
