namespace AccountMe.Models
{
    public class Account : IRepositoryItem
    {

        public int Id { get; set; }
        
        public string? Name { get; set; }

        public IEnumerable<User> Users { get; set; } = new List<User>();

        public int GetKey()
        {
           return Id;
        }
    }
}
