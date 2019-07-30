using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Game;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.Game.GetGameById
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameViewModel>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetGameByIdQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GameViewModel> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Games
                .Include(g => g.GameCategories).ThenInclude(g => g.Category)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Domain.Models.Game), request.Id);
            }

            return mapper.Map<GameViewModel>(entity);
        }
    }
}
