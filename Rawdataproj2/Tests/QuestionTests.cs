using DataService;
using DataService.Dto.QuestionDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebService;
using WebService.Controllers;
using Xunit;

namespace UnitTesting
{
    public class QuestionTests
    {
        [Fact]
        public async Task GetQuestion_ValidQuestionId_OkObjectResult()
        {
            int testQuestionId = 19;
            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IQuestionRepository>();

            dataService
                .Setup(repo => repo.GetQuestion(testQuestionId))
                .Returns(Task.FromResult((Question)new Question()));
              
            var urlHelper = new Mock<IUrlHelper>();
            var controller = new QuestionController(dataService.Object, mapper);
            controller.Url = urlHelper.Object;

            var result = await controller.GetQuestion(testQuestionId); 
            Assert.IsType<OkObjectResult>(result); 
            dataService.Verify(x => x.GetQuestion(testQuestionId)); 



        }
        // /*https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing */
        [Fact]
        public async Task GetQuestion_InvalidQuestionId_NotFound()
        {
            int testQuestionId = -19;
            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IQuestionRepository>();

            dataService
                .Setup(repo => repo.GetQuestion(testQuestionId))
                .Returns(Task.FromResult((Question)null));

            var urlHelper = new Mock<IUrlHelper>();
            var controller = new QuestionController(dataService.Object, mapper);
            controller.Url = urlHelper.Object;

            var result = await controller.GetQuestion(testQuestionId);
            Assert.IsType<NotFoundResult>(result);
            dataService.Verify(x => x.GetQuestion(testQuestionId));

        }
    }
}


