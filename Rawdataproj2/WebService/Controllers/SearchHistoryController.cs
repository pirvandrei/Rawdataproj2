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

        [HttpGet("{id}", Name = nameof(GetHistory))]
        public async Task<IActionResult> GetHistory(PagingInfo pagingInfo)
        {
            var history = await _SearchHistoryRepository.GetAll(pagingInfo);

            IEnumerable<SearchHistoryListModel> model = history.Select(search => SearchHistoryListModel(search));

            var total = _SearchHistoryRepository.Count();
            var pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);

            var prev = pagingInfo.Page > 0
                ? Url.Link(nameof(GetHistory),
                    new { page = pagingInfo.Page - 1, pagingInfo.PageSize })
                : null;

            var next = pagingInfo.Page < pages - 1
                ? Url.Link(nameof(GetHistory),
                    new { page = pagingInfo.Page + 1, pagingInfo.PageSize })
                : null;

            var result = new
            {
                Prev = prev,
                Next = next,
                Total = total,
                Pages = pages,
                History = model
            };

            return Ok(result);  
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearch(int id)
        {
            if (!await _SearchHistoryRepository.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSearch([FromBody]CreateHistoryModel model)
        {
            if (model == null) return BadRequest();

            var search = new Search
            {
                Text = model.Text,
                Date = model.Date, 
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
