using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IDataRepository<TEntity, in TKey> where TEntity : class
    {
         Task<TEntity> Get(TKey id);
        Task<TEntity> Get(TKey userId, TKey entityId);
        Task<IEnumerable<TEntity>> GetAll(PagingInfo pagingInfo);
        
        Task<bool> Update(TKey id, TEntity b);
        Task<bool> Delete(TKey id);
        void Add(TEntity b);
        int Count();
    }
}
