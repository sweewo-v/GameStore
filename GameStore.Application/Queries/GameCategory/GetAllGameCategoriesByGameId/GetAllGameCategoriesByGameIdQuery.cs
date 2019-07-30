using System.Collections.Generic;
using GameStore.Application.ViewModels.GameCategory;
using MediatR;

namespace GameStore.Application.Queries.GameCategory.GetAllGameCategoriesByGameId
{
    public class GetAllGameCategoriesByGameIdQuery : IRequest<IEnumerable<GameCategoryByGameIdViewModel>>
    {
        public int Id { get; set; }
    }
}
