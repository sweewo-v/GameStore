using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IGameStoreDbContext context;

        public DeleteCategoryCommandHandler(IGameStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Categories
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }
            context.Categories.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
