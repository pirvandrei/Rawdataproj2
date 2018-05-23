using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface ICommentRepository : IDataService<Comment, int>
    {
    }
}
