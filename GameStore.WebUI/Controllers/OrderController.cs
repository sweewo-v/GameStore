using System.Linq;
using System.Threading.Tasks;
using GameStore.Application.Commands.Order.CreateOrder;
using GameStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly Cart cart;

        public OrderController(Cart cart)
        {
            this.cart = cart;
        }

        public ViewResult Checkout() => View(new CreateOrderCommand());

        [HttpPost]
        public async Task<IActionResult> Checkout([FromForm]CreateOrderCommand createOrderCommand)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
         
            if (ModelState.IsValid)
            {
                createOrderCommand.Lines = cart.Lines;
                await Mediator.Send(createOrderCommand);
                return RedirectToAction(nameof(Completed));
            }

            return View(createOrderCommand);
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
