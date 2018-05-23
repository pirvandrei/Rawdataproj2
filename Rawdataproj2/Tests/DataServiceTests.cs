using DataService;
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
        public void GetAllPosts_NoArgument_ReturnsAllPosts()
        {
            //var repo = new PostRepository();

            var info = new PagingInfo()
            {
                PageSize = 2
            };

            //var questions = repo.GetAll(info);
            //Assert.Equal(2, questions.Result.Count());
            // Assert.Equal(164, questions.Result.First().Score);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsPostsObject()
        {
            //var repo = new PostRepository();
            //var question = repo.Get(19);
            //Assert.Equal(164, question.Result.Score);
        }

        [Fact]
        public void GetPost_WithAnswerId()
        {
            var repo = new QuestionRepository();
            var question = repo.GetQuestion(19);
            Assert.Equal(531, question.Result.AcceptedAnswerID);
        }


        //SELECT count(*) FROM raw2.Posts where ParentID = 19;
        [Fact]
        public void GetPost_WithAnswers()
        {
            var repo = new QuestionRepository();
            var question = repo.GetQuestion(19);
            Assert.Equal(21, question.Result.Answers.Count());
        }


        //SELECT count(c.ID) FROM  Comments as c join Posts as p  on  c.PostID  =  p.Id where p.id = 19  ;
        //16

        public void GetPost_WithAnswersandComments()
        {
            var repo = new QuestionRepository();
            var question = repo.GetQuestion(19);
            Assert.Equal(16, question.Result.Comments.Count());
             
        }
    }
    }
