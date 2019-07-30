using AutoMapper;
using GameStore.Application.Interfaces.Mapping;
using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Game;
using GameStore.Domain.Models;

namespace GameStore.Application.ViewModels.Cart
{
    public class CartLineViewModel : ICustomMapping
    {
        public int Id { get; set; }
        public GameViewModel Game { get; set; }
        public int Quantity { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<OrderLine, CartLineViewModel>();
        }
    }
}