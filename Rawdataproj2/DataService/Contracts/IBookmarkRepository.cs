using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IBookmarkRepository : IDataService<Bookmark, int>
    {
        string GetAnswerTitle(int iD);
    }
}
