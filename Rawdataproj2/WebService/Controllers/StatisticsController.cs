using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DomainModel;
using DataService;
using WebService.Models.Statistics;
using DataService.Dto.StatisticsDto;

namespace WebService.Controllers
{
    [Route("api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsRepository _StatisticsRepository;
        private readonly IMapper _Mapper;

        public StatisticsController(IStatisticsRepository StatisticsRepository, IMapper Mapper)
        {
            _StatisticsRepository = StatisticsRepository;
            _Mapper = Mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Use either the rankedwordlist, weightedwordlist, associations or termnetwork for retrieving statistics");
        }

        [HttpGet("rankedwordlist", Name = nameof(RankedWordList))] 
        public async Task<IActionResult> RankedWordList(string word)
        {
            if (string.IsNullOrEmpty(word)) return Ok("No word parameter given");

            var list =  await _StatisticsRepository.RankedWordList(word);
            if (list == null || list.Count <= 0) return NotFound("Nothing matched the query");

            var model = list.Select(x => CreateRankedWordListModel(x));
             
            return  Ok(model);
        }

        [HttpGet("weightedwordlist", Name = nameof(WeightedWordList))]
        public async Task<IActionResult> WeightedWordList(string term)
        {
            if (string.IsNullOrEmpty(term)) return Ok("No term parameter given");

            var list = await _StatisticsRepository.WeightedWordList(term);
            if (list == null || list.Count <= 0) return NotFound("Nothing matched the query");

            var model = list.Select(x => CreateWeightedWordListModel(x));

            return Ok(model);
        }

        [HttpGet("associations", Name = nameof(GetAssociations))]
        public async Task<IActionResult> GetAssociations(string word)
        {
            if (string.IsNullOrEmpty(word)) return Ok("No word parameter");

            var list = await _StatisticsRepository.GetAssociations(word);
            if (list == null || list.Count <= 0) return NotFound("Nothing matched the query");

            var model = list.Select(x => CreateAssociationsListModel(x));

            return Ok(model);
        }

        [HttpGet("termnetwork", Name = nameof(TermNetwork))]
        public async Task<IActionResult> TermNetwork(string word, double grade)
        {
            if (string.IsNullOrEmpty(word)) return Ok("No word parameter");

            var cleanedWord = CleanString(word);

            var graph = await _StatisticsRepository.TermNetwork(cleanedWord, grade);
            if (graph == null) return NotFound("Nothing matched the query");

            var model = CreateTermNetworkModel(graph);

            return Ok(model);
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private RankedWordListModel CreateRankedWordListModel(RankedWordListDto dto)
        {
            var model = _Mapper.Map<RankedWordListModel>(dto);

            return model;
        }

        private WeightedWordListModel CreateWeightedWordListModel(WeightedWordListDto dto)
        {
            var model = _Mapper.Map<WeightedWordListModel>(dto);

            return model;
        }

        private AssociationsListModel CreateAssociationsListModel(AssociationsListDto dto)
        {
            var model = _Mapper.Map<AssociationsListModel>(dto);

            return model;
        }

        private TermNetworkModel CreateTermNetworkModel(TermNetworkDto dto)
        {
            var model = _Mapper.Map<TermNetworkModel>(dto);

            return model;
        }

        private string CleanString(string word)
        {
            var charsToRemove = new string[] { @"\", "\"" };
            foreach (var c in charsToRemove)
            {
                word = word.Replace(c, string.Empty);
            }
            return word;
        }


    }
}