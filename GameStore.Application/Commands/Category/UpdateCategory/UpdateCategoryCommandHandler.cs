using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IGameStoreDbContext context;

        public UpdateCategoryCommandHandler(IGameStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Categories
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
