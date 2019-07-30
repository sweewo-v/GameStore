using System.Collections.Generic;
using GameStore.Domain.Models;
using MediatR;

namespace GameStore.Application.Commands.Order.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public string Name { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public bool GiftWrap { get; set; }

        public IEnumerable<OrderLine> Lines { get; set; }
    }
}
