using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Interfaces;
using GameStore.Domain.Common;
using MediatR;

namespace GameStore.Application.Commands.Game.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
    {
        private readonly IGameStoreDbContext context;
        private readonly IDateTime dateTime;

        public CreateGameCommandHandler(IGameStoreDbContext context, IDateTime dateTime)
        {
            this.context = context;
            this.dateTime = dateTime;
        }

        public async Task<Unit> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Models.Game
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                AddedDateTime = dateTime.Now
            };

            context.Games.Add(entity);

            request.SelectedCategories = request.SelectedCategories ?? Enumerable.Empty<int>();
            var gameCategories = request.SelectedCategories.ToList().Select(
                u => new Domain.Models.GameCategory { GameId = entity.Id, CategoryId = u });
            context.GameCategories.AddRange(gameCategories);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
