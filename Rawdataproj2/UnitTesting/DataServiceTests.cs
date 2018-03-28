using DomainModel;
using StackoverflowContext;
using System;
using System.Linq;
using Xunit;

namespace UnitTesting
{
    public class DataServiceTests
<<<<<<< HEAD
    { 
=======
    {
        [Fact]
        public void Post_Object_HasIdNameAndDescription()
        {
            var post = new Post();
            Assert.Equal(0, post.ID);
            Assert.Null(post.Body);
            Assert.Equal(0, post.Score); 
        }
>>>>>>> c9604d714f427017fb46f480456c012e710c2e9a

        [Fact]
        public void GetAllPosts_NoArgument_ReturnsAllCategories()
        {
            var service = new StackoverflowDbDataservice();
            var questions = service.GetQuestions();
            Assert.Equal(13629, questions.Count);
            Assert.Equal(164, questions.First().Score);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsCategoryObject()
        {
            var service = new StackoverflowDbDataservice();
            var post = service.GetQuestion(19);
            Assert.Equal(164, post.Score);
        }
<<<<<<< HEAD
          
       
=======

        //[Fact]
        //public void CreatePost_ValidData_CreteCategoryAndRetunsNewObject()
        //{
        //    var service = new StackoverflowDbDataservice();
        //    var post = service.CreatePost(13, "Test", 2);
        //    Assert.True(post.ID > 0);
        //    Assert.Equal("Test", post.Body);
        //    Assert.Equal(2, post.Score);

        //    // cleanup
        //    service.DeleteQuestion(post.ID);
        //}

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
>>>>>>> c9604d714f427017fb46f480456c012e710c2e9a
    }
}
