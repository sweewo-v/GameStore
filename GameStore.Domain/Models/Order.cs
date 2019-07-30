using System;
using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public ICollection<OrderLine> Lines { get; set; }

        public string Name { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public bool GiftWrap { get; set; }

        public DateTime AddedDateTime { get; set; }
    }
}