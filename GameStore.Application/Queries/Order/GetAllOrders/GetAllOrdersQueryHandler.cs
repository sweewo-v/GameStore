using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.ViewModels;
using GameStore.Application.ViewModels.Order;
using GameStore.Application.ViewModels.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PageViewModel<OrderViewModel>>
    {
        private readonly IGameStoreDbContext context;
        private readonly IMapper mapper;

        public GetAllOrdersQueryHandler(IGameStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PageViewModel<OrderViewModel>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var query = context.Orders;

            var count = await query.CountAsync(cancellationToken: cancellationToken);
            var items = await query
                .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            var itemsEnumerable = mapper.Map<IEnumerable<OrderViewModel>>(items);
            var res = new PageViewModel<OrderViewModel>(itemsEnumerable, request.Pagination.Page,
                request.Pagination.PageSize, count);

            return res;
        }
    }
}
