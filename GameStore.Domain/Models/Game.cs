using System;
using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime AddedDateTime { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }

        public ICollection<OrderLine> Lines { get; set; }
    }
}
