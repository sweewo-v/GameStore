using System;
using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime AddedDateTime { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
