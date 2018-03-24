using DataAccessLayer.Dto.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IPostRepository : IDataRepository<PostDto, int>
    {
        // add post specific methods here - similar approach with the other entities

        IList<PostDto> GetPostByName(string s);

    }
}
