using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRepository;
using DataRepository.Dto.SearchDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // TODO: return SearchHistoryListModel instead.
            return Ok(await _SearchHistoryRepository.GetAll());
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private SearchHistoryListModel CreateSearchHistoryListModel(SearchDto history)
        {
            var model = new SearchHistoryListModel
            {
                Text = history.Text,
                Date = history.Date
            };
            return model;
        }

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
