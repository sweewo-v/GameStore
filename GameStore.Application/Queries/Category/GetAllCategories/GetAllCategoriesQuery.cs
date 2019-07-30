using System.Collections.Generic;
using GameStore.Application.ViewModels.Category;
using MediatR;

namespace GameStore.Application.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryViewModel>>
    {
    }
}
