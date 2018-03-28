using DomainModel;
using StackoverflowContext;
using System;
using System.Linq;
using Xunit;

namespace UnitTesting
{
    public class DataServiceTests
    { 

        [Fact]
        public void GetAllPosts_NoArgument_ReturnsAllCategories()
        {
            var service = new StackoverflowDbDataservice();
            var posts = service.GetPosts();
            Assert.Equal(13629, posts.Count);
            Assert.Equal(164, posts.First().Score);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsCategoryObject()
        {
            var service = new StackoverflowDbDataservice();
            var post = service.GetPost(19);
            Assert.Equal(164, post.Score);
        }
          
       
    }
}
