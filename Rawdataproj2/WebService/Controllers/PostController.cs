using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using DataRepository.Dto.PostDto;
using DataRepository;
using WebService.Models.Post;
using AutoMapper;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository _PostRepository;
        private readonly IMapper _Mapper;

        public PostController(IPostRepository PostRepository, IMapper Mapper)
        {
            _PostRepository = PostRepository;
            _Mapper = Mapper;
        }

        [HttpGet(Name = nameof(GetPosts))]
        public async Task<IActionResult> GetPosts(PagingInfo pagingInfo)
        {
            var posts = await _PostRepository.GetAll(pagingInfo);  
            IEnumerable<PostListModel> model = posts.Select(post => CreatePostListModel(post));

            var total = _PostRepository.Count();
            var prev = Url.Link(nameof(GetPosts), new { page = pagingInfo.Page - 1, pagingInfo.PageSize });
            var next = Url.Link(nameof(GetPosts), new { page = pagingInfo.Page + 1, pagingInfo.PageSize });

            var returnType = new ReturnTypeConstants("posts");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);

            return Ok(result); 
        }


        [HttpGet("{id}", Name = nameof(GetPost))]
        public async Task<IActionResult> GetPost(int id)
        {
            var p = await _PostRepository.Get(id);
            if (p == null) return NotFound();

            var model = CreatePostModel(p);

            return Ok(model); 
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private PostListModel CreatePostListModel(Post post)
        {
            var model = new PostListModel
            {
                Score = post.Score,
                Type = post.PostType
            };
            model.Url = CreateLink(post.ID);
            return model;
        }

        private PostModel CreatePostModel(Post post)
        {
            var model = _Mapper.Map<PostModel>(post);
            model.Url = CreateLink(post.ID);
            return model;
        }
        
        private string CreateLink(int id)
        {
            return Url.Link(nameof(GetPost), new { id });
        }

    } 
}
