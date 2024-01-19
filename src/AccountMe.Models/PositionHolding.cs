namespace AccountMe.Models
{
    public class PositionHolding : IRepositoryItem
    {

        public int Id { get; set; }

        public Account? Account { get; set; }

        public Position? Position { get; set; }

        public decimal Quota { get; set; }

        public int GetKey()
        {
            return Id;
        }

    }
}
