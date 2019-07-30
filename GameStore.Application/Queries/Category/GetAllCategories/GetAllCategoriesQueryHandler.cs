using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryViewModel>>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetAllCategoriesQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);

            var list = mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            return list;
        }
    }
}
