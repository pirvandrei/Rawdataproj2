using DataRepository.Dto.PostDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public interface IPostRepository : IDataRepository<QuestionDto, int>
    {
        // add post specific methods here - similar approach with the other entities

        IList<QuestionDto> GetPostByName(string s);

    }
}
