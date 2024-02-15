namespace AccountMe.Models
{
    public interface IRepository
    {

        public Task<IEnumerable<IRepositoryItem>> GetAllAsync(Type itemType);

        public Task<IRepositoryItem>? GetByKey(Type itemType, int key);

        public Task<IRepositoryItem> Insert(IRepositoryItem entity);

        public Task<IRepositoryItem> Update(IRepositoryItem entity);

        public Task Delete(IRepositoryItem entity);

    }
}
