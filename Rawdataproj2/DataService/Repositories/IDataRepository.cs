using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IDataRepository<TEntity, in TKey> where TEntity : class
    {
        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll(PagingInfo pagingInfo);
        Task<TEntity> Add(TEntity b);
        Task<bool> Update(TEntity b);
        Task<bool> Delete(TKey id);
        int Count();
    }
}
