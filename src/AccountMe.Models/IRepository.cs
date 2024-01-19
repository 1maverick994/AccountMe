namespace AccountMe.Models
{
    public interface IRepository
    {

        public Task<IEnumerable<IRepositoryItem>> GetAllAsync();

        public Task<IRepositoryItem>? GetByKey(int key);

        public Task<IRepositoryItem> Insert(IRepositoryItem entity);

        public Task<IRepositoryItem> Update(IRepositoryItem entity);

        public Task Delete(IRepositoryItem entity);

    }
}
