namespace GameStore.Domain.Models
{
    public class GameCategory
    {
        public Game Game { get; set; }

        public int GameId { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as GameCategory;

            if (item is null)
            {
                return false;
            }

            return GameId.Equals(item.GameId)
                && CategoryId.Equals(item.CategoryId);
        }

        public override int GetHashCode()
        {
            return GameId.GetHashCode() + CategoryId.GetHashCode();
        }
    }
}
