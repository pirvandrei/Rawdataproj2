using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace UnitTesting.IntegrationTesting
{
    public class SearchTests
    { 
        private const string BestmatchSearchApi = " http://localhost:59831/api/search/bestmatch?query=";
        private const string MatchAllSearchApi = " http://localhost:59831/api/search/matchall?query=";
        private const string BestMatchRankedSearchApi = " http://localhost:59831/api/search/bestmatchranked?query=";
        private const string BestMatchWeightedSearchApi = " http://localhost:59831/api/search/bestmatchweighted?query=";

        //-----------BestMatchWeightedSearchApi------------------
        //dotnet test --filter BestMatchWeightedSearchApi_GetSearchWithValidQuery_OkAndListOfBestMatchRankedSearchDto
        [Fact]
        public void BestMatchWeightedSearchApi_GetSearchWithValidQuery_OkAndListOfBestMatchWeightedSearchDto()
        {
            //var query = "\'Hanselman\'";
            var url = BestMatchWeightedSearchApi + "\'Hanselman\'";
            var (post, statusCode) = Helpers.GetArray($"{url}");

            //var count = 

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, post.Count);
            Assert.IsType<double>(((JValue)post.FirstOrDefault()["rank"]).Value);
            Assert.IsType<Int64>(((JValue)post.FirstOrDefault()["id"]).Value); 

        }

        //dotnet test --filter FullyQualifiedName~BestMatchWeightedSearchApi_GetSearchWithValidQuery_NotFound
        [Fact]
        public void BestMatchWeightedSearchApi_GetSearchWithValidQuery_NotFound()
        {
            var (post, statusCode) = Helpers.GetArray($"{BestMatchRankedSearchApi}'Hanssselmanssssssss'");

            Assert.Equal(0, post.Count);
        }


        //-----------BestMatchRankedSearchApi------------------
        //dotnet test --filter BestMatchRankedSearchApi_GetSearchWithValidQuery_OkAndListOfBestMatchRankedSearchDto
        [Fact]
        public void BestMatchRankedSearchApi_GetSearchWithValidQuery_OkAndListOfBestMatchRankedSearchDto()
        {
            //var query = "\'Hanselman\'";
            var url = BestMatchRankedSearchApi + "\'Hanselman\'";
            var (post, statusCode) = Helpers.GetArray($"{url}");

            //var count = 

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, post.Count);
            Assert.IsType<double>(((JValue)post.FirstOrDefault()["rank"]).Value);
            Assert.IsType<Int64>(((JValue)post.FirstOrDefault()["id"]).Value); 

        }

        //dotnet test --filter FullyQualifiedName~BestMatchRankedSearchApi_GetSearchWithValidQuery_NotFound
        [Fact]
        public void BestMatchRankedSearchApi_GetSearchWithValidQuery_NotFound()
        {
            var (post, statusCode) = Helpers.GetArray($"{BestMatchRankedSearchApi}'Hanssselmanssssssss'");

            Assert.Equal(0, post.Count);
        }


        //-----------MatchAllSearchApi------------------
        //dotnet test --filter MatchAllSearchApi_GetSearchWithValidQuery_OkAndListOfMatchAllDto
        [Fact]
        public void MatchAllSearchApi_GetSearchWithValidQuery_OkAndListOfMatchAllDto()
        {
            //var query = "\'Hanselman\'";
            var url = MatchAllSearchApi + "\'Hanselman\'";
            var (post, statusCode) = Helpers.GetArray($"{url}");

            //var count = 

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, post.Count); 
            Assert.IsType<double>(((JValue)post.FirstOrDefault()["rank"]).Value);
            Assert.IsType<Int64>(((JValue)post.FirstOrDefault()["id"]).Value);
            Assert.IsType<string>(((JValue)post.FirstOrDefault()["body"]).Value);

        }

        //dotnet test --filter FullyQualifiedName~MatchAllSearchApi_GetSearchWithValidQuery_NotFound
        [Fact]
        public void MatchAllSearchApi_GetSearchWithValidQuery_NotFound()
        {
            var (post, statusCode) = Helpers.GetArray($"{MatchAllSearchApi}'Hanssselmanssssssss'");

            Assert.Equal(0, post.Count);
        }


        //-----------BestmatchSearchApi------------------
        //dotnet test --filter FullyQualifiedName~BestmatchSearchApi_GetWithValidPostId_OkAnd
        [Fact]
        public void BestmatchSearchApi_GetSearchWithValidQuery_OkAndSearchJsonModel()
        {  
            var (post, statusCode) = Helpers.GetObject($"{BestmatchSearchApi}'Hanselman'");
             
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, post["total"]);
        }

        //dotnet test --filter FullyQualifiedName~BestmatchSearchApi_GetSearchWithValidQuery_OkAndEmpty
        [Fact]
        public void BestmatchSearchApi_GetSearchWithValidQuery_OkAndEmpty()
        {
            var (post, statusCode) = Helpers.GetObject($"{BestmatchSearchApi}'Hanssselmanssssssss'");

            Assert.Equal(0, post["total"]);
        }




    }
}