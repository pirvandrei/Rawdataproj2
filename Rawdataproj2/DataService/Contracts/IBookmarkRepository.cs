using DataService.Dto.BookmarkDto;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IBookmarkRepository : IDataService<Bookmark, int>
    { 
		Task<IEnumerable<BookmarkDto>> GetBookmarks(PagingInfo pagingInfo);
    }
}
