using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.ViewModels;
using GameStore.Application.ViewModels.Game;
using GameStore.Application.ViewModels.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.Game.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, PageViewModel<GameViewModel>>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetAllGamesQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PageViewModel<GameViewModel>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var query = context.Games
                .Include(g => g.GameCategories)
                .ThenInclude(c => c.Category)
                .Where(p => request.Category == null
                            || p.GameCategories.Any(c =>
                                c.Category.Name.Equals(request.Category, StringComparison.OrdinalIgnoreCase)));

            var count = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);
            var gamesEnumerable = mapper.Map<IEnumerable<GameViewModel>>(items);
            var res = new PageViewModel<GameViewModel>(gamesEnumerable,
                request.Pagination.Page, request.Pagination.PageSize, count);

            return res;
        }
    }
}
