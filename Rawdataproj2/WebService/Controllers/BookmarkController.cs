using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DomainModel;
using WebService.Models.Bookmark;
using DataRepository;
using DataRepository.Dto.BookmarkDto;

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

        [HttpGet("{id}", Name = nameof(GetBookmark))] 
        public async Task<ActionResult> GetBookmark(int id)
        { 

            var question =  await _BookmarkRepository.Get(id); 
            if (question == null) return NotFound(); 

            var model = _Mapper.Map<BookmarkModel>(question);
             
            return  Ok(model);
        }
         
        [HttpGet(Name = nameof(GetBookmarks))]
        public async Task<IActionResult> GetBookmarks(PagingInfo pagingInfo)
        {
            var bookmark = await _BookmarkRepository.GetAll(pagingInfo);
            IEnumerable<BookmarkListModel> model = bookmark.Select(que => CreateBookmarkListModel(que));

            var total = _BookmarkRepository.Count();
            var pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);

            var prev = pagingInfo.Page > 0
                                 ? Url.Link(nameof(GetBookmark),
                    new { page = pagingInfo.Page - 1, pagingInfo.PageSize })
                : null;

            var next = pagingInfo.Page < pages - 1
                                 ? Url.Link(nameof(GetBookmark),
                    new { page = pagingInfo.Page + 1, pagingInfo.PageSize })
                : null;

            var result = new
            {
                Prev = prev,
                Next = next,
                Total = total,
                Pages = pages,
                Questions = model
            }; 

            return Ok(result);
        }

        // POST api/<controller>
        //[HttpPost]
        //public async Task<IActionResult>  Add([FromBody]string value)
        //{
            
        //}

        // DELETE api/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
        }


        //[HttpGet("{id}/answers", Name = nameof(GetQuestionAnswers))]
        //public async Task<IActionResult> GetQuestionAnswers(int id)
        //{
        //    var queAnswers = await _BookmarkRepository.GetQuestionAnswers(id);
        //    if (queAnswers == null) return NotFound();

        //    var model = queAnswers.Select(que => CreateQuestionAnswersModel(que)); 

        //    return Ok(model);
        //}


        //[HttpGet("{id}/comments", Name = nameof(GetQuestionComments))]
        //public async Task<IActionResult> GetQuestionComments(int id)
        //{
        //    var queCom = await _BookmarkRepository.GetQuestionComments(id);
        //    if (queCom == null) return NotFound();

        //    var model = queCom.Select(que => CreateQuestionCommnetsModel(que));

        //    return Ok(model);
        //}

 



        /*******************************************************
         * Helpers
         * *****************************************************/
       

        private BookmarkListModel CreateBookmarkListModel(Bookmark bookmark)
        {
            var model = new BookmarkListModel
            {
                  PostID = bookmark.PostID, 
            };
            model.Url = CreateBookmarkLink(bookmark.UserID);
            return model;
        }
          
        private string CreateBookmarkLink(int id)
        {
            return Url.Link(nameof(GetBookmark), new { id });
        }

        //private string CreateAnswerLink(int id)
        //{
        //    return Url.Link(nameof(GetAnswer), new { id });
        //}

      
    }
}