using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface INoteRepository : IDataRepository<Note, int>
    {
       
    }
}
