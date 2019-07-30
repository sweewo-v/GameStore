using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Queries.Game.GetGameById;
using GameStore.Application.ViewModels.Cart;
using GameStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class CartController : BaseController
    {
        public CartController(Cart cart, IMapper mapper)
        {
            this.cart = cart;
            this.mapper = mapper;
        }

        private readonly Cart cart;
        private readonly IMapper mapper;

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> AddToCart(int gameId, string returnUrl)
        {
            var game = await Mediator.Send(new GetGameByIdQuery { Id = gameId });

            if (game != null)
            {
                cart.AddItem(mapper.Map<Game>(game), 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> RemoveFromCart(int gameId, string returnUrl)
        {
            var game = await Mediator.Send(new GetGameByIdQuery { Id = gameId });

            if (game != null)
            {
                cart.RemoveLine(mapper.Map<Game>(game));
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}