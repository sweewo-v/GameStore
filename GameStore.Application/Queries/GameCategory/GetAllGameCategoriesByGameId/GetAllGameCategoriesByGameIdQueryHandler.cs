using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.ViewModels.GameCategory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.GameCategory.GetAllGameCategoriesByGameId
{
    public class GetAllGameCategoriesByGameIdQueryHandler :
        IRequestHandler<GetAllGameCategoriesByGameIdQuery, IEnumerable<GameCategoryByGameIdViewModel>>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetAllGameCategoriesByGameIdQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameCategoryByGameIdViewModel>> Handle(GetAllGameCategoriesByGameIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.GameCategories
                .Where(g => g.GameId.Equals(request.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            return mapper.Map<IEnumerable<GameCategoryByGameIdViewModel>>(entity);
        }
    }
}
