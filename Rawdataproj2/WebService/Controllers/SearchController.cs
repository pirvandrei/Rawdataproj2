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
         
        [HttpGet("{query}", Name = nameof(Bestmatch))]
        public async Task<ActionResult> Bestmatch(string query)
        {
            //api/Search/'"","",""'
            var search = await _SearchRepository.Bestmatch(query);
            if (search == null) return NotFound();

           // var model = _Mapper.Map<BestmatchModel>(search);
            var model = search.Select(s => CreateBestmatchModel(s));
            return Ok(model);
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