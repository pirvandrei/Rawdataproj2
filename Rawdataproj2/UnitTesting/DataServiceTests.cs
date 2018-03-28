using DomainModel;
using StackoverflowContext;
using System;
using System.Linq;
using Xunit;

namespace UnitTesting
{
    public class DataServiceTests { 

        [Fact]
        public void GetAllPosts_NoArgument_ReturnsAllCategories()
        {
            var service = new StackoverflowDbDataservice();
            var questions = service.GetQuestions();
            Assert.Equal(2237, questions.Count);
            Assert.Equal(164, questions.First().Score);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsCategoryObject()
        {
            var service = new StackoverflowDbDataservice();
            var question = service.GetQuestion(19);
            Assert.Equal(164, question.Score);
        } 
    }
}
