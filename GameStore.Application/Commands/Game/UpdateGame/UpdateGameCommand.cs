using System.Collections.Generic;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.ViewModels.Category;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Commands.Game.UpdateGame
{
    public class UpdateGameCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }

        public IEnumerable<CategoryViewModel> AllCategories { get; set; }
    }
}
