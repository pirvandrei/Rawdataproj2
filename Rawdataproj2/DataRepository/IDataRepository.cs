using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IDataRepository<TEntity, in TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(TKey id);
        void Add(TEntity b);
        Task<bool> Update(TKey id, TEntity b);
        Task<bool> Delete(TKey id);
    }
}
