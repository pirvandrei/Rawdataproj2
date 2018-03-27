using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Controller
{
    [Route("api/posts")]
    public class PostController
    {
        StackoverflowDbDataservice _dataService = new StackoverflowDbDataservice();


        [HttpGet]
        public List<Post> Get()
        {
            return _dataService.GetPosts();
        }


        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return _dataService.GetPost(id);
        }
    }


     
}
