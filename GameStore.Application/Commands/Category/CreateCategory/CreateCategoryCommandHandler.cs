using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Interfaces;
using GameStore.Domain.Common;
using MediatR;

namespace GameStore.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IGameStoreDbContext context;
        private readonly IDateTime dateTime;

        public CreateCategoryCommandHandler(IGameStoreDbContext context, IDateTime dateTime)
        {
            this.context = context;
            this.dateTime = dateTime;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Models.Category
            {
                Name = request.Name,
                AddedDateTime = dateTime.Now
            };

            context.Categories.Add(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
