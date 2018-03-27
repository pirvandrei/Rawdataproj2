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
        public void Post_Object_HasIdNameAndDescription()
        {
            var post = new Post();
            Assert.Equal(0, post.Id);
            Assert.Null(post.Body);
            Assert.Equal(0, post.Score); 
        }

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

        [Fact]
        public void CreatePost_ValidData_CreteCategoryAndRetunsNewObject()
        {
            var service = new StackoverflowDbDataservice();
            var post = service.CreatePost(13, "Test", 2);
            Assert.True(post.Id > 0);
            Assert.Equal("Test", post.Body);
            Assert.Equal(2, post.Score);

            // cleanup
            service.DeletePost(post.Id);
        }

        //[Fact]
        //public void DeleteCategory_ValidId_RemoveTheCategory()
        //{
        //    var service = new StackoverflowDbDataservice();
        //    var category = service.CreateCategory("Test", "DeleteCategory_ValidId_RemoveTheCategory");
        //    var result = service.DeleteCategory(category.Id);
        //    Assert.True(result);
        //    category = service.GetCategory(category.Id);
        //    Assert.Null(category);
        //}

        //[Fact]
        //public void DeleteCategory_InvalidId_ReturnsFalse()
        //{
        //    var service = new StackoverflowDbDataservice();
        //    var result = service.DeleteCategory(-1);
        //    Assert.False(result);
        //}

        //[Fact]
        //public void UpdateCategory_NewNameAndDescription_UpdateWithNewValues()
        //{
        //    var service = new StackoverflowDbDataservice();
        //    var category = service.CreateCategory("TestingUpdate", "UpdateCategory_NewNameAndDescription_UpdateWithNewValues");

        //    var result = service.UpdateCategory(category.Id, "UpdatedName", "UpdatedDescription");
        //    Assert.True(result);

        //    category = service.GetCategory(category.Id);

        //    Assert.Equal("UpdatedName", category.Name);
        //    Assert.Equal("UpdatedDescription", category.Description);

        //    // cleanup
        //    service.DeleteCategory(category.Id);
        //}

        //[Fact]
        //public void UpdateCategory_InvalidID_ReturnsFalse()
        //{
        //    var service = new StackoverflowDbDataservice();
        //    var result = service.UpdateCategory(-1, "UpdatedName", "UpdatedDescription");
        //    Assert.False(result);

        //}
    }
}
