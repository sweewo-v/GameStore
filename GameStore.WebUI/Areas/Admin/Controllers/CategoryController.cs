using System.Threading.Tasks;
using GameStore.Application.Commands.Category.CreateCategory;
using GameStore.Application.Commands.Category.DeleteCategory;
using GameStore.Application.Commands.Category.UpdateCategory;
using GameStore.Application.Exceptions;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.Queries.Category.GetCategoryById;
using GameStore.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var values = await Mediator.Send(new GetAllCategoriesQuery());
            return View(values);
        }

        public ActionResult Create()
        {
            return View(new CreateCategoryCommand
            {
                Name = "Category name"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]CreateCategoryCommand createGameCategoryCommand)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(createGameCategoryCommand);
                TempData["message"] =
                    $"Category \"{createGameCategoryCommand.Name}\" has been saved.";

                return RedirectToAction(nameof(Index));
            }

            return View(createGameCategoryCommand);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            try
            {
                var category = await Mediator.Send(new GetCategoryByIdQuery { Id = id.Value });
                var categoryUpdateCommand = new UpdateCategoryCommand
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return View(categoryUpdateCommand);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm]UpdateCategoryCommand updateGameCategoryCommand)
        {
            if (id != updateGameCategoryCommand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(updateGameCategoryCommand);
                    TempData["message"] =
                        $"Category \"{updateGameCategoryCommand.Name}\" has been updated.";

                    return RedirectToAction(nameof(Index));
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            try
            {
                var category = await Mediator.Send(new GetCategoryByIdQuery { Id = id.Value });

                return View(category);
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
                await Mediator.Send(new DeleteCategoryCommand { Id = id });
                TempData["message"] = $"Category with id \"{id}\" has been deleted.";

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}