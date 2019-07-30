using System.Collections.Generic;
using System.Linq;

namespace GameStore.Domain.Models
{
    public class Cart
    {
        private readonly List<OrderLine> _lineCollection = new List<OrderLine>();

        public virtual void AddItem(Game game, int quantity)
        {
            var line = _lineCollection
                .FirstOrDefault(p => p.Game.Id == game.Id);

            if (line is null)
            {
                _lineCollection.Add(new OrderLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Game game)
        {
            _lineCollection.RemoveAll(l => l.Game.Id == game.Id);

        }

        public virtual decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Game.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            _lineCollection.Clear();
        }

        public virtual IEnumerable<OrderLine> Lines => _lineCollection;
    }

}
