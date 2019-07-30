using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Commands.Game.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGameStoreDbContext context;

        public DeleteGameCommandHandler(IGameStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Games.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Domain.Models.Game), request.Id);
            }
            context.Games.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
