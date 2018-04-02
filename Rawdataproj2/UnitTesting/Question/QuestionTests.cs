using DataRepository;
using DataRepository.Dto.QuestionDto;
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
        public async Task GetQuestion_ValidQuestionId_NotFound()
        {
            int testQuestionId = 19;
            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IQuestionRepository>();

            dataService
                .Setup(repo => repo.Get(testQuestionId))
                .Returns(Task.FromResult((Question)null));
              
            var urlHelper = new Mock<IUrlHelper>();
            var ctrl = new QuestionController(dataService.Object, mapper);
            ctrl.Url = urlHelper.Object;

            var result = await ctrl.GetQuestion(testQuestionId); 
            Assert.IsType<OkObjectResult>(result); 
            dataService.Verify(x => x.Get(testQuestionId)); 



        }
        // /*https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing */
        [Fact]
        public async Task GetQuestion_InvalidQuestionId_NotFound()
        {
            int testQuestionId = -1;
            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IQuestionRepository>();

            dataService
               .Setup(repo => repo.Get(testQuestionId))
               .Returns(Task.FromResult((Question)null));

            var urlHelper = new Mock<IUrlHelper>();
            var ctrl = new QuestionController(dataService.Object, mapper);
            ctrl.Url = urlHelper.Object;

            var result = await ctrl.GetQuestion(testQuestionId);
            Assert.IsType<NotFoundResult>(result);
            dataService.Verify(x => x.Get(testQuestionId));
        } 
    }
}


