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

namespace UnitTesting.Integrationtesting
{
    public class Integrationtesting
    { 
        private const string NotesApi = " http://localhost:59831/api/notes";
        private const string BookmarkApi = " http://localhost:59831/api/notes";
        private const string ProductsApi = "http://localhost:5001/api/products";




        /* /api/categories */
        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithNoArguments_OkAndAllNots
        //[Fact]
        //public void ApiNotes_GetWithNoArguments_OkAndAllNots()
        //{

        //    var client = new HttpClient();
        //    var response = client.GetAsync(NotestApi).Result; 
        //    var data = response.Content.ReadAsStringAsync().Result;
        //    var result = JsonConvert.DeserializeObject(data);
        //    var array = (JArray)result["notes"];
        //    var resResponse = response.StatusCode;


        //    //var (data, statusCode) = GetArray(NotestApi);

        //    Assert.Equal(HttpStatusCode.OK, resResponse);
        //     Assert.Equal(2, result.Count);
        //   Assert.Equal("Testing", result.First()["text"]); 
        //}


        //-----------Note------------------
        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithValidNoteId_OkAndNote
        [Fact]
        public void ApiNotes_GetWithValidPostId_OkAndNote()
        {
            var (post, statusCode) = GetObject($"{NotesApi}/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("testing", post["text"]);
        }

        //dotnet test --filter FullyQualifiedName~ApiNotes_GetWithInvalidPostId_NotFound
        [Fact]
        public void ApiNotes_GetWithInvalidPostId_NotFound()
        {
            var (note, statusCode) = GetObject($"{NotesApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        //-----------Bookmark------------------
        //dotnet test --filter FullyQualifiedName~ApiBookmarks_GetWithValidPostId_OkAndNote
        [Fact]
        public void ApiBookmarks_GetWithValidPostId_OkAndNote()
        {
            var (bookmark, statusCode) = GetObject($"{BookmarkApi}/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("testing", bookmark["text"]);
        }

        //dotnet test --filter FullyQualifiedName~ApiBookmarks_GetWithInvalidPostId_NotFound
        [Fact]
        public void ApiBookmarks_GetWithInvalidPostId_NotFound()
        {
            var (bookmark, statusCode) = GetObject($"{BookmarkApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }


        //[Fact]
        //public void ApiCategories_PostWithCategory_Created()
        //{
        //    var newCategory = new
        //    {
        //        Name = "Created",
        //        Description = ""
        //    };
        //    var (category, statusCode) = PostData(NotesApi, newCategory);

        //    Assert.Equal(HttpStatusCode.Created, statusCode);

        //    DeleteData($"{NotesApi}/{category["id"]}");
        //}

        //[Fact]
        //public void ApiCategories_PutWithValidCategory_Ok()
        //{

        //    var data = new
        //    {
        //        Name = "Created",
        //        Description = "Created"
        //    };
        //    var (category, _) = PostData($"{NotesApi}", data);

        //    var update = new
        //    {
        //        Id = category["id"],
        //        Name = category["name"] + "Updated",
        //        Description = category["description"] + "Updated"
        //    };

        //    var statusCode = PutData($"{NotesApi}/{category["id"]}", update);

        //    Assert.Equal(HttpStatusCode.OK, statusCode);

        //    var (cat, status) = GetObject($"{NotesApi}/{category["id"]}");

        //    Assert.Equal(category["name"] + "Updated", cat["name"]);
        //    Assert.Equal(category["description"] + "Updated", cat["description"]);

        //    DeleteData($"{NotesApi}/{category["id"]}");
        //}

        //[Fact]
        //public void ApiCategories_PutWithInvalidCategory_NotFound()
        //{
        //    var update = new
        //    {
        //        Id = -1,
        //        Name = "Updated",
        //        Description = "Updated"
        //    };

        //    var statusCode = PutData($"{NotesApi}/-1", update);

        //    Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //}

        //[Fact]
        //public void ApiCategories_DeleteWithValidId_Ok()
        //{

        //    var data = new
        //    {
        //        Name = "Created",
        //        Description = "Created"
        //    };
        //    var (category, _) = PostData($"{NotesApi}", data);

        //    var statusCode = DeleteData($"{NotesApi}/{category["id"]}");

        //    Assert.Equal(HttpStatusCode.OK, statusCode);
        //}

        //[Fact]
        //public void ApiCategories_DeleteWithInvalidId_NotFound()
        //{

        //    var statusCode = DeleteData($"{NotesApi}/-1");

        //    Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //}

        ///* /api/products */

        //[Fact]
        //public void ApiProducts_ValidId_CompleteProduct()
        //{
        //    var (product, statusCode) = GetObject($"{ProductsApi}/1");

        //    Assert.Equal(HttpStatusCode.OK, statusCode);
        //    Assert.Equal("Chai", product["name"]);
        //    Assert.Equal("Beverages", product["category"]["name"]);
        //}

        //[Fact]
        //public void ApiProducts_InvalidId_CompleteProduct()
        //{
        //    var (product, statusCode) = GetObject($"{ProductsApi}/-1");

        //    Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //}

        //[Fact]
        //public void ApiProducts_CategoryValidId_ListOfProduct()
        //{
        //    var (products, statusCode) = GetArray($"{ProductsApi}/category/1");

        //    Assert.Equal(HttpStatusCode.OK, statusCode);
        //    Assert.Equal(12, products.Count);
        //    Assert.Equal("Chai", products.First()["name"]);
        //    Assert.Equal("Beverages", products.First()["categoryName"]);
        //    Assert.Equal("Lakkalikööri", products.Last()["name"]);
        //}

        //[Fact]
        //public void ApiProducts_CategoryInvalidId_EmptyListOfProductAndNotFound()
        //{
        //    var (products, statusCode) = GetArray($"{ProductsApi}/category/1000001");

        //    Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //    Assert.Equal(0, products.Count);
        //}

        //[Fact]
        //public void ApiProducts_NameContained_ListOfProduct()
        //{
        //    var (products, statusCode) = GetArray($"{ProductsApi}/name/ant");

        //    Assert.Equal(HttpStatusCode.OK, statusCode);
        //    Assert.Equal(3, products.Count);
        //    Assert.Equal("Chef Anton's Cajun Seasoning", products.First()["productName"]);
        //    Assert.Equal("Guaraná Fantástica", products.Last()["productName"]);
        //}

        //[Fact]
        //public void ApiProducts_NameNotContained_EmptyListOfProductAndNotFound()
        //{
        //    var (products, statusCode) = GetArray($"{ProductsApi}/name/RAWDATA");

        //    Assert.Equal(HttpStatusCode.NotFound, statusCode);
        //    Assert.Equal(0, products.Count);
        //}



        // Helpers

        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
    }
}