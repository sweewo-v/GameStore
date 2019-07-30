namespace GameStore.Domain.Models
{
    public class OrderLine
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Game Game { get; set; }

        public int GameId { get; set; }

        public int Quantity { get; set; }
    }
}
