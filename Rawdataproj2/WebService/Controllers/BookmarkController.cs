using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DomainModel;
using WebService.Models.Bookmark;
using DataService;
using DataService.Dto.BookmarkDto;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/bookmarks")]
    public class BookmarkController : Controller
    {
        private readonly IBookmarkRepository _BookmarkRepository;
        private readonly IMapper _Mapper;

        public BookmarkController(IBookmarkRepository BookmarkRepository, IMapper Mapper)
        {
            _BookmarkRepository = BookmarkRepository;
            _Mapper = Mapper;
        }

        [HttpGet(Name = nameof(GetBookmarks))]
        public async Task<IActionResult> GetBookmarks(PagingInfo pagingInfo)
        {
            //var bookmark = await _BookmarkRepository.GetAll(pagingInfo);
            var bookmark = await _BookmarkRepository.GetBookmarks(pagingInfo);
            var model = bookmark.Select(que => CreateBookmarkListModel(que));

            var total = _BookmarkRepository.Count();
            var prev = Url.Link(nameof(GetBookmarks), new { page = pagingInfo.Page - 1, pagingInfo.PageSize }).ToLower();
            var next = Url.Link(nameof(GetBookmarks), new { page = pagingInfo.Page + 1, pagingInfo.PageSize }).ToLower();

            var returnType = new ReturnTypeConstants("bookmarks");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetBookmark))]
        public async Task<IActionResult> GetBookmark(int id)
        {
            var bookmark = await _BookmarkRepository.Get(id);
            if (bookmark == null) return NotFound();

            var model = _Mapper.Map<BookmarkModel>(bookmark);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            if (!await _BookmarkRepository.Delete(id)) return NotFound();
            return Json(NoContent());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateBookmark([FromBody] CreateBookmarkModel model)
        {
            if (model == null) return BadRequest();

            var bookmark = new Bookmark
            {
                PostID = model.PostID,
                UserID = model.UserID
            };

            var result = await _BookmarkRepository.Add(bookmark);

            return Json(Ok(result));
        }



        /*******************************************************
         * Helpers
         * *****************************************************/


        private BookmarkListModel CreateBookmarkListModel(BookmarkDto bookmark)
        {
            var model = new BookmarkListModel
            {
                Type = bookmark.Posttype,
                ParentID = bookmark.ParentID,
                PostID = bookmark.PostID,
                Title = bookmark.Title,
                UserID = bookmark.UserID,

            };
            model.Url = CreateBookmarkLink(bookmark.PostID);
            return model;
        }

        private string CreateBookmarkLink(int id)
        {
            return Url.Link(nameof(GetBookmark), new { id });
        }

    }
}