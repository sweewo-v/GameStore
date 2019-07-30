using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Commands.Game.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IGameStoreDbContext context;

        public UpdateGameCommandHandler(IGameStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Games
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Domain.Models.Game), request.Id);
            }

            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.Name = request.Name;

            request.SelectedCategories = request.SelectedCategories ?? Enumerable.Empty<int>();
            var gameCategories = request.SelectedCategories.ToList().Select(
                u => new Domain.Models.GameCategory { GameId = entity.Id, CategoryId = u });
            var savedGameCategories = await context.GameCategories
                .Where(gc => gc.GameId == entity.Id).ToListAsync(cancellationToken: cancellationToken);

            var resCategories = gameCategories.Except(savedGameCategories);
            context.GameCategories.AddRange(resCategories);

            resCategories = savedGameCategories.Except(gameCategories);
            context.GameCategories.RemoveRange(resCategories);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
