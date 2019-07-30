using MediatR;

namespace GameStore.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string Name { get; set; }
    }

}
