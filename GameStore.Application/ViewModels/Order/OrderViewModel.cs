using System;
using AutoMapper;
using GameStore.Application.Interfaces.Mapping;

namespace GameStore.Application.ViewModels.Order
{
    public class OrderViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public DateTime AddedDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.Order, OrderViewModel>();
        }
    }
}
