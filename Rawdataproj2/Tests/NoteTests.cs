using DataService;
using DataService.Dto.QuestionDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class NoteTests
    {
        //[Fact]
        //public async Task GetNote_ValidNoteId_OkObjectResult()
        //{
        //    int testNoteUserID = 1;
        //    int testNotePostID = 19;

        //    var mapper = MapperCollectionExtension.CreateMapper();
        //    var dataService = new Mock<INoteRepository>();
        //    //var logger = new ILogger<INoteRepository>();

        //dataService
        //        .Setup(repo => repo.Get(testNotePostID))
        //        .Returns(Task.FromResult((Note)new Note()));

        //    var urlHelper = new Mock<IUrlHelper>();
        //    var controller = new NoteController(dataService.Object, mapper );
        //    controller.Url = urlHelper.Object;

        //    var result = await controller.GetNote(testNotePostID);
        //    Assert.IsType<OkObjectResult>(result);
        //    dataService.Verify(x => x.Get(testNotePostID));

        //}
        //// /*https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing */
        //[Fact]
        //public async Task GetNode_InvalidNodeId_NotFound()
        //{
        //    int testNoteUserID = -1;
        //    int testNotePostID = -19;

        //    var mapper = MapperCollectionExtension.CreateMapper();
        //    var dataService = new Mock<INoteRepository>();

        //    dataService
        //        .Setup(repo => repo.Get(testNotePostID))
        //        .Returns(Task.FromResult((Note)null));

        //    var urlHelper = new Mock<IUrlHelper>();
        //    var controller = new NoteController(dataService.Object, mapper);
        //    controller.Url = urlHelper.Object;

        //    var result = await controller.GetNote(testNotePostID);
        //    Assert.IsType<NotFoundResult>(result);
        //    dataService.Verify(x => x.Get(testNotePostID));

        //}

        [Fact]
        public async Task GetBookmark_ValidBookmarkId_OkObjectResult()
        { 
            int testBookmarkPostID = 19;

            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IBookmarkRepository>();

            dataService
                .Setup(repo => repo.Get(testBookmarkPostID))
                .Returns(Task.FromResult((Bookmark)new Bookmark()));

            var urlHelper = new Mock<IUrlHelper>();
            var controller = new BookmarkController(dataService.Object, mapper);
            controller.Url = urlHelper.Object;

            var result = await controller.GetBookmark(testBookmarkPostID);
            Assert.IsType<OkObjectResult>(result);
            dataService.Verify(x => x.Get(testBookmarkPostID));

        }
        // /*https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing */
        [Fact]
        public async Task GetBookmark_ValidBookmarkId_NotFound()
        {
            int testBookmarkPostID = -1; 

            var mapper = MapperCollectionExtension.CreateMapper();
            var dataService = new Mock<IBookmarkRepository>();

            dataService
                .Setup(repo => repo.Get(testBookmarkPostID))
                .Returns(Task.FromResult((Bookmark)null));

            var urlHelper = new Mock<IUrlHelper>();
            var controller = new BookmarkController(dataService.Object, mapper);
            controller.Url = urlHelper.Object;

            var result = await controller.GetBookmark(testBookmarkPostID);
            Assert.IsType<NotFoundResult>(result);
            dataService.Verify(x => x.Get(testBookmarkPostID));

        }
    }
}



