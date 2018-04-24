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
    [Route("api/Search")]
    public class SearchController : Controller
    {
        private readonly ISearchRepository _SearchRepository;
        private readonly IMapper _Mapper;

        public SearchController(ISearchRepository SearchRepository, IMapper Mapper)
        {
            _SearchRepository = SearchRepository;
            _Mapper = Mapper;
        }
         
        [HttpGet(Name = nameof(Bestmatch))]
        public async Task<ActionResult> Bestmatch(string query, PagingInfo pagingInfo)
        {
            var search = await _SearchRepository.Bestmatch(query, pagingInfo);
            if (search == null && search.Count <= 0) return NotFound();

            var model = search.Select(s => CreateBestmatchModel(s));

            var total = search.Count();
            var prev = Url.Link(nameof(Bestmatch), new { page = pagingInfo.Page - 1, pagingInfo.PageSize });
            var next = Url.Link(nameof(Bestmatch), new { page = pagingInfo.Page + 1, pagingInfo.PageSize });
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, "Search on posts", prev, next);

            return Ok(result);
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
    }
}