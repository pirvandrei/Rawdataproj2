using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using DataService.Dto.SearchDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Models.Search;

namespace WebService.Controllers
{
    [Route("api/history")]
    public class SearchHistoryController : Controller
    {
        private readonly ISearchHistoryRepository _SearchHistoryRepository;

        public SearchHistoryController(ISearchHistoryRepository SearchHistoryRepository)
        {
            _SearchHistoryRepository = SearchHistoryRepository;
        }

        [HttpGet("{userid}", Name = nameof(GetHistory))]
        public async Task<IActionResult> GetHistory(int userid, PagingInfo pagingInfo)
        {
            var history = await _SearchHistoryRepository.GetSearchHistory(userid, pagingInfo);

            var model = history.Select(search => SearchHistoryListModel(search));

            var total = _SearchHistoryRepository.Count();
            var prev = Url.Link(nameof(GetHistory), new { page = pagingInfo.Page - 1, pagingInfo.PageSize }).ToLower();
            var next = Url.Link(nameof(GetHistory), new { page = pagingInfo.Page + 1, pagingInfo.PageSize }).ToLower();

            var returnType = new ReturnTypeConstants("searchhistory");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);

            return Ok(result);  
        } 

        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteSearch(int userid, string text)
        {
            if (!await _SearchHistoryRepository.Delete(userid)) return NotFound();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSearch([FromBody]CreateHistoryModel model)
        {
            if (model == null) return BadRequest();

            var search = new Search
            {
                Text = model.Text,
                Date = DateTime.Now,
                UserID = model.UserID
            };

            var result = await _SearchHistoryRepository.Add(search);

            return Ok(result);
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private SearchHistoryListModel SearchHistoryListModel(Search history)
        {
            var model = new SearchHistoryListModel
            {
                Text = history.Text,
                Date = history.Date
            };
            return model;
        }

        

    }
}
