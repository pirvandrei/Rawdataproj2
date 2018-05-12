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
    public class IntegrationTesting
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

        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithInvalidPostId_NotFound
        [Fact]
        public void ApiNotes_GetWithInvalidPostId_NotFound()
        {
            var (note, statusCode) = Helpers.GetObject($"{NotesApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        //-----------Bookmark------------------
        //dotnet test --filter FullyQualifiedName~ApiBookmarks_GetWithValidPostId_OkAndNote
        [Fact]
        public void ApiBookmarks_GetWithValidPostId_OkAndNote()
        {
            var (bookmark, statusCode) = Helpers.GetObject($"{BookmarkApi}/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("testing", bookmark["text"]);
        }

        //dotnet test --filter FullyQualifiedName~ApiBookmarks_GetWithInvalidPostId_NotFound
        [Fact]
        public void ApiBookmarks_GetWithInvalidPostId_NotFound()
        {
            var (bookmark, statusCode) = Helpers.GetObject($"{BookmarkApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }


        
    }
}