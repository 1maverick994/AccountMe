using System.Collections.Generic;
using System.Linq;

namespace AccountMe.Service.Tests
{
    public class Repository : IRepository
    {
        
        private Dictionary<int, IRepositoryItem> _repository = new Dictionary<int, IRepositoryItem>();

        public Task Delete(IRepositoryItem entity)
        {
            if (_repository.ContainsKey(entity.GetKey()))
                _repository.Remove(entity.GetKey());
            else
                throw new KeyNotFoundException();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<IRepositoryItem>> GetAllAsync()
        {
            return Task.FromResult(Enumerable.AsEnumerable(_repository.Values));
        }

        public Task<IRepositoryItem>? GetByKey(int key)
        {
            if (!_repository.ContainsKey(key))
                return null;
           return Task.FromResult(_repository[key]);
        }

        public Task<IRepositoryItem> Insert(IRepositoryItem entity)
        {
            _repository.Add(entity.GetKey(), entity);
            return Task.FromResult(entity);
        }

        public Task<IRepositoryItem> Update(IRepositoryItem entity)
        {
            if (_repository.ContainsKey(entity.GetKey()))
                return Task.FromResult(entity);
            else
                throw new KeyNotFoundException();
        }

       
      

     
    }
}