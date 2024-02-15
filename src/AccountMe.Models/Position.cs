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

        public Position()
        {
                
        }


        public Position(int id, string name, decimal balance)
        {
            this.Id = id;
            this.Name = name;
            this.Balance = balance;
        }
    }
}
