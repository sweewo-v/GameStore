using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Interfaces;
using GameStore.Domain.Common;
using GameStore.Domain.Models;
using MediatR;

namespace GameStore.Application.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IGameStoreDbContext context;
        private readonly IDateTime dateTime;

        public CreateOrderCommandHandler(IGameStoreDbContext context, IDateTime dateTime)
        {
            this.context = context;
            this.dateTime = dateTime;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Models.Order
            {
                Name = request.Name,
                Line1 = request.Line1,
                Line2 = request.Line2,
                Line3 = request.Line3,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                Country = request.Country,
                GiftWrap = request.GiftWrap,
                AddedDateTime = dateTime.Now
            };
            context.Orders.Add(entity);

            var lines = request.Lines.Select(s =>
                new OrderLine { GameId = s.Game.Id, OrderId = entity.Id, Quantity = s.Quantity });
            context.OrderLines.AddRange(lines);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
