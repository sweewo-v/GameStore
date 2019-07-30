using GameStore.Application.ViewModels;
using GameStore.Application.ViewModels.Order;
using GameStore.Application.ViewModels.Pagination;
using MediatR;

namespace GameStore.Application.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PageViewModel<OrderViewModel>>
    {
        public PageRequestViewModel Pagination { get; set; }
    }
}
