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
    public class BestMatchTest
    { 
        private const string SearchApi = " http://localhost:59831/api/search?query="; 

        //-----------Note------------------
        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithValidPostId_OkAnd
        [Fact]
        public void ApiSearch_GetSearchWithValidQuery_OkAndSearchJsonModel()
        {
            var (post, statusCode) = Helpers.GetObject($"{SearchApi}'Hanselman'");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, post["total"]);
        }

        //dotnet test --filter FullyQualifiedName~ApiSearch_GetSearchWithValidQuery_OkAndEmpty
        [Fact]
        public void ApiSearch_GetSearchWithValidQuery_OkAndEmpty()
        {
            var (post, statusCode) = Helpers.GetObject($"{SearchApi}'Hanssselmanssssssss'");

            Assert.Equal(0, post["total"]);
        }




    }
}