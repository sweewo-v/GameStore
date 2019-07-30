using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.ViewModels.Category;
using MediatR;

namespace GameStore.Application.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryViewModel>
    {
        public int Id { get; set; }
    }
}
