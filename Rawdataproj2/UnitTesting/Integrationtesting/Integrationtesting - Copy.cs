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
        private const string NotesApi = " http://localhost:59831/api/notes";
        private const string BookmarkApi = " http://localhost:59831/api/notes";  
        private const string ProductsApi = "http://localhost:5001/api/products";

        //-----------Note------------------
        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithValidNoteId_OkAndNote
        [Fact]
        public void ApiNotes_GetWithValidPostId_OkAndNote()
        {
            var (post, statusCode) = Helpers.GetObject($"{NotesApi}/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("testing", post["text"]);
        }

         


        
    }
}