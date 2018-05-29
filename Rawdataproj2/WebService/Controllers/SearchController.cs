using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using DataService; 
using DataService.Dto.SearchDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Models.Search;

namespace WebService.Controllers
{  
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly ISearchRepository _SearchRepository;

        public SearchController(ISearchRepository SearchRepository, IMapper Mapper)
        {
            _SearchRepository = SearchRepository;
        }

        [HttpGet(Name = nameof(Search))]
        public async Task<IActionResult> Search(string query, PagingInfo pagingInfo, string method = null, string startdate = null, string enddate = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                return Ok("No query provided");
            }

            var cleanedQuery = CleanString(query);
            
            var getStartDate = string.IsNullOrEmpty(startdate) ? startdate = "'2000-01-01'" : startdate;
            var getEndDate = string.IsNullOrEmpty(enddate) ? enddate = "'" + DateTime.Today.ToString("yyyy-MM-dd") + "'" : enddate;

            var search = await UseFetchingMethod(cleanedQuery, pagingInfo, method, getStartDate, getEndDate);
            if (search == null || search.Item2 <= 0) return NotFound();

            var model = search.Item1.Select(s => CreateSearchResultModel(s));

            var urls = GetUrls(query, pagingInfo, method, getStartDate, getEndDate);
            var prev = urls[0];
            var next = urls[1];
            var total = search.Item2;

            var returnType = new ReturnTypeConstants("posts");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);
          
            return Ok(result);
        }


        /*******************************************************
         * Helpers
         * *****************************************************/

        private async Task<Tuple<IList<SearchResultDto>, int>> UseFetchingMethod(string query, PagingInfo pagingInfo, string method, string startDate, string endDate)
        {
            IList<SearchResultDto> result = new List<SearchResultDto>();
            var numberOfRows = 0;

            switch (method)
            {
                case "\"\"":
                    var option1 = await _SearchRepository.BestMatchRanked(query, pagingInfo, startDate, endDate);
                    result = option1.Item1;
                    numberOfRows = option1.Item2;
                    break;
                case "\"bestmatchranked\"":
                    var option2 = await _SearchRepository.BestMatchRanked(query, pagingInfo, startDate, endDate);
                    result = option2.Item1;
                    numberOfRows = option2.Item2;
                    break;
                case "\"matchall\"":
                    var option3 = await _SearchRepository.MatchAll(query, pagingInfo, startDate, endDate);
                    result = option3.Item1;
                    numberOfRows = option3.Item2;
                    break;
                case "\"bestmatchweighted\"":
                    var option4 = await _SearchRepository.BestMatchWeighted(query, pagingInfo, startDate, endDate);
                    result = option4.Item1;
                    numberOfRows = option4.Item2;
                    break;
                default:
                    var defaultOption = await _SearchRepository.BestMatchRanked(query, pagingInfo, startDate, endDate);
                    result = defaultOption.Item1;
                    numberOfRows = defaultOption.Item2;
                    break;
            }

            return new Tuple<IList<SearchResultDto>, int>(result, numberOfRows);
        }

        private string[] GetUrls(string query, PagingInfo pagingInfo, string method, string startDate, string endDate)
        {
            var prev = Url.Link(nameof(Search), new
            {
                query,
                method = string.IsNullOrEmpty(method) ? "" : method,
                startdate = string.IsNullOrEmpty(startDate) ? "" : startDate,
                enddate = string.IsNullOrEmpty(endDate) ? "" : endDate,
                page = pagingInfo.Page - 1, pagingInfo.PageSize
            }).ToLower();

            var next = Url.Link(nameof(Search), new
            {
                query,
                method = string.IsNullOrEmpty(method) ? "" : method,
                startdate = string.IsNullOrEmpty(startDate) ? "" : startDate,
                enddate = string.IsNullOrEmpty(endDate) ? "" : endDate,
                page = pagingInfo.Page + 1, pagingInfo.PageSize
            }).ToLower();

            var urls = new string[] { prev, next };

            return urls;
        }

        private SearchResultModel CreateSearchResultModel(SearchResultDto search)
        {
            var model = new SearchResultModel
            {
                Id = search.Id,
                Title = search.Title,
                PostType = search.PostType,
                CreationDate = search.CreationDate,
                AcceptedAnswerId = search.AcceptedAnswerId,
                Rank = search.Rank,
                Body = search.Body
            };
            return model;
        }

        private string CleanString(string query)
        {
            var charsToRemove = new string[] { @"\", "\"" };

            foreach (var c in charsToRemove)
            {
                query = query.Replace(c, string.Empty);
            }

            var regex = new Regex("(?<=\")[^\"]*(?=\")|[^\" ]+");
            var words = regex.Matches(query).Cast<Match>().Select(m => m.Value).ToArray();

            var sb = new StringBuilder();
            string lastWord = words[words.Length - 1];

            foreach (var w in words)
            {
                sb.Append("'" + w + "'");

                if(w != lastWord)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

    }
}