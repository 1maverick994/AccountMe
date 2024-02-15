using System.Collections.Generic;
using System.Linq;

namespace AccountMe.Service.Tests
{
    public class Repository : IRepository
    {
        
        private Dictionary<Type, Dictionary<int, IRepositoryItem>> _repository = new Dictionary<Type, Dictionary<int, IRepositoryItem>>();

        public Task Delete(IRepositoryItem entity)
        {
            var type = entity.GetType();

            if(_repository.ContainsKey(type) && _repository[type].ContainsKey(entity.GetKey()))
                _repository[type].Remove(entity.GetKey());                            
            else
                throw new KeyNotFoundException();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<IRepositoryItem>> GetAllAsync(Type itemType)
        {
           
            if(_repository.ContainsKey(itemType))
                return Task.FromResult(Enumerable.AsEnumerable(_repository[itemType].Values));
            else
                return Task.FromResult(Enumerable.Empty<IRepositoryItem>());    
        }

        public Task<IRepositoryItem>? GetByKey(Type itemType, int key)
        {
            if (!_repository.ContainsKey(itemType) || !_repository[itemType].ContainsKey(key))
                return null;
            
           return Task.FromResult(_repository[itemType][key]);
        }

        public Task<IRepositoryItem> Insert(IRepositoryItem entity)
        {
            var type = entity.GetType();
            
            if (!_repository.ContainsKey(type)) 
                    _repository.Add(type, new Dictionary<int, IRepositoryItem>());
            
            _repository[type].Add(entity.GetKey(), entity);
            
            return Task.FromResult(entity);
        }

        public Task<IRepositoryItem> Update(IRepositoryItem entity)
        {
            var type = entity.GetType();

            if (_repository.ContainsKey(type) && _repository[type].ContainsKey(entity.GetKey()))
                return Task.FromResult(entity);
            else
                throw new KeyNotFoundException();
        }

     
     
    
      
    }
}