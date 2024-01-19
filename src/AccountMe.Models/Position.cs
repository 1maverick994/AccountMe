namespace AccountMe.Models
{
    public class Position : IRepositoryItem
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Balance { get; set; }

        public IEnumerable<PositionHolding> Holdings { get; set; } = Enumerable.Empty<PositionHolding>();

        public int GetKey()
        {
            return Id;
        }
    }
}
