using DomainModel;
using StackoverflowContext;
using System;
using System.Linq;
using Xunit;

namespace UnitTesting
{
    public class DataServiceTests { 

        [Fact]
        public void GetAllPosts_NoArgument_ReturnsAllPosts()
        {
            var service = new StackoverflowDbDataservice();
            var questions = service.GetQuestions();
            Assert.Equal(2237, questions.Count);
            Assert.Equal(164, questions.First().Score);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsPostsObject()
        {
            var service = new StackoverflowDbDataservice();
            var question = service.GetQuestion(19);
            Assert.Equal(164, question.Score);
        }

        [Fact]
        public void GetPost_WithAnswerId()
        {
            var service = new StackoverflowDbDataservice();
            var question = service.GetQuestion(19);
            Assert.Equal(531, question.AcceptedAnswerID);
        }


        //SELECT count(*) FROM raw2.Posts where ParentID = 19;
        [Fact]
        public void GetPost_WithAnswers()
        {
            var service = new StackoverflowDbDataservice();
            var question = service.GetQuestion(19);
            Assert.Equal(21, question.Answers.Count());
        }

    }
}
