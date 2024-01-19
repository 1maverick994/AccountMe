namespace AccountMe.Models
{
    public class Transaction : IRepositoryItem
    {

        public int Id { get; set; }

        public Position? PositionIn { get; set; }

        public Position? PositionOut { get; set; }

        public decimal Amount { get; set; }

        public Category? Category { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
