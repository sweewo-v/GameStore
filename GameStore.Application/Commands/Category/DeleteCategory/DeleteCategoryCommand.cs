using MediatR;

namespace GameStore.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
