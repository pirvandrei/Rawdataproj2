using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataRepository; 
using DataRepository.Dto.SearchDto;
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
        private readonly IMapper _Mapper;

        public SearchController(ISearchRepository SearchRepository, IMapper Mapper)
        {
            _SearchRepository = SearchRepository;
            _Mapper = Mapper;
        }

        [HttpGet(Name = nameof(Search))]
        public async Task<IActionResult> Search(string query, PagingInfo pagingInfo, string method = null, string startdate = null, string enddate = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                return Ok("No query provided");
            }
            
            var search = await UseFetchingMethod(query, pagingInfo, method, startdate, enddate);

            //if (search == null || search.Item2 <= 0) return NotFound("Nothing matched your query");

            var model = search.Item1.Select(s => CreateSearchResultModel(s));

            var urls = GetUrls(query, pagingInfo, method, startdate, enddate);
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
            IList<SearchResultDto> search = new List<SearchResultDto>();
            int numberOfRows = 0;
            switch (method)
            {
                case "\"\"":
                    var option1 = await _SearchRepository.Bestmatch(query, pagingInfo, method, startDate, endDate);
                    option1.ToList().ForEach(s => { search.Add(s); });
                    break;
                case "\"bestmatch\"":
                    var option2 = await _SearchRepository.Bestmatch(query, pagingInfo, method, startDate, endDate);
                    option2.ToList().ForEach(s => { search.Add(s); });
                    break;
                case "\"matchall\"":
                    var option3 = await _SearchRepository.MatchAll(query, pagingInfo, method, startDate, endDate);
                    option3.Item1.ToList().ForEach(s => { search.Add(s); });
                    numberOfRows = option3.Item2;
                    break;
                default:
                    var defaultOption = await _SearchRepository.Bestmatch(query, pagingInfo, method, startDate, endDate);
                    defaultOption.ToList().ForEach(s => { search.Add(s); });
                    break;
            }
            return new Tuple<IList<SearchResultDto>, int>(search, numberOfRows);
        }

        private string[] GetUrls(string query, PagingInfo pagingInfo, string method, string startDate, string endDate)
        {
            var prev = Url.Link(nameof(Search), new
            {
                query,
                method = string.IsNullOrEmpty(method) ? "" : method,
                startdate = string.IsNullOrEmpty(startDate) ? "" : startDate,
                orderby = string.IsNullOrEmpty(endDate) ? "" : endDate,
                page = pagingInfo.Page - 1, pagingInfo.PageSize
            });

            var next = Url.Link(nameof(Search), new
            {
                query,
                method = string.IsNullOrEmpty(method) ? "" : method,
                sortby = string.IsNullOrEmpty(startDate) ? "" : startDate,
                orderby = string.IsNullOrEmpty(endDate) ? "" : endDate,
                page = pagingInfo.Page + 1, pagingInfo.PageSize
            });

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

        private BestmatchModel CreateBestmatchModel(BestmatchDto search)
        {
            var model = new BestmatchModel
            {
                Id = search.Id,
                Rank = search.Rank
            };
            return model;
        }


        private MatchallModel CreateMatchAllhModel(MatchallDto search)
        {
            var model = new MatchallModel
            {
                Id = search.Id,
                Rank = search.Rank,
                Body = search.Body
            };
            return model;
        }

        private BestmatchRankedModel CreateBestMatchRankedModel(BestMatchRankedDto search)
        {
            var model = new BestmatchRankedModel
            {
                Id = search.Id,
                Rank = search.Rank 
            };
            return model;
        }

        private BestMatchWeightedModel CreateBestMatchWeightedModel(BestMatchWeightedDto search)
        {
            var model = new BestMatchWeightedModel
            {
                Id = search.Id,
                Rank = search.Rank 
            };
            return model;
        }

    }
}