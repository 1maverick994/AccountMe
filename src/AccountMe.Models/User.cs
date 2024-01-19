namespace AccountMe.Models
{
    public class User : IRepositoryItem
    {

        public int Id { get; set; }

        public string? Username { get; set; }

        public Tenant? Tenant { get; set; }

        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();

        public int GetKey()
        {
            return Id;
        }
    }
}
