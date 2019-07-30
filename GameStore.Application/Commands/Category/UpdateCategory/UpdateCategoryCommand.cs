using MediatR;

namespace GameStore.Application.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}