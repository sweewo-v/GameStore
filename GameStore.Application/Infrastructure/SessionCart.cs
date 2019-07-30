using System;
using GameStore.Application.Infrastructure.Extensions;
using GameStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GameStore.Application.Infrastructure
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        [JsonIgnore]
        private const string CartName = "Cart";

        public static Cart GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var cart = session?.GetJson<SessionCart>(CartName)
                               ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(Game game, int quantity)
        {
            base.AddItem(game, quantity);
            Session.SetJson(CartName, this);
        }

        public override void RemoveLine(Game game)
        {
            base.RemoveLine(game);
            Session.SetJson(CartName, this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove(CartName);
        }
    }
}