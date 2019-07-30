using System.Collections.Generic;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Category;
using GameStore.Application.ViewModels.Pagination;

namespace GameStore.Application.ViewModels.Game
{
    public class GamesListViewModel
    {
        public PageViewModel<GameViewModel> Games { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
