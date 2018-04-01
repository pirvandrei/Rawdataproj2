using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public interface INoteRepository : IDataRepository<Note, int>
    { 
    }
}
