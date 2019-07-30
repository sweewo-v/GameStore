using System.Collections.Generic;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.ViewModels.Category;
using MediatR;

namespace GameStore.Application.Commands.Game.CreateGame
{
    public class CreateGameCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }

        public IEnumerable<CategoryViewModel> AllCategories { get; set; }

        public decimal Price { get; set; }
    }
}