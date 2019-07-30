using System.Threading.Tasks;
using GameStore.Application.Queries.Order.GetAllOrders;
using GameStore.Application.ViewModels.Pagination;
using GameStore.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : BaseController
    {
        public async Task<IActionResult> Index(int? p)
        {
            var values = await Mediator.Send(new GetAllOrdersQuery
            {
                Pagination = new PageRequestViewModel(p ?? 1, 5),
            });

            return View(values);
        }

    }
}