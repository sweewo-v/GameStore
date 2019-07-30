using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryViewModel>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetCategoryByIdQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Categories
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            return mapper.Map<CategoryViewModel>(entity);
        }
    }
}
