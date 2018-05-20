using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public interface ICommentRepository : IDataRepository<Comment, int>
    {
    }
}
