
namespace AccountMe.Models
{
    public class Category : IRepositoryItem
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Category? Parent { get; set; }

        public IEnumerable<Category>? Children { get; set; } = new List<Category>();

        public int GetKey()
        {
            return Id;
        }
    }
}
