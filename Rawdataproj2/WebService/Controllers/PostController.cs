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
        public async Task<ActionResult> GetPosts(PagingInfo pagingInfo)
        {
            var posts = await _PostRepository.GetAll(pagingInfo);  
            IEnumerable<PostListModel> model = posts.Select(post => CreatePostListModel(post));

            //var products = _dataService
            //   .GetProducts(pagingInfo)
            //   .Select(CreateProductListModel);


            var total = _PostRepository.Count();
            var pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);

            var prev = pagingInfo.Page > 0
                ? Url.Link(nameof(GetPosts),
                    new { page = pagingInfo.Page - 1, pagingInfo.PageSize })
                : null;

            var next = pagingInfo.Page < pages - 1
                ? Url.Link(nameof(GetPosts),
                    new { page = pagingInfo.Page + 1, pagingInfo.PageSize })
                : null;

            var result = new
            {
                Prev = prev,
                Next = next,
                Total = total,
                Pages = pages,
                Questions = model
            };


            return Ok(result); 
        }


        [HttpGet("{id}", Name = nameof(GetPost))]
        public async Task<ActionResult> GetPost(int id)
        {
            var postDto = _PostRepository.Get(id);
            if (postDto == null) return NotFound();

            PostModel model = new PostModel
            {
                Url = CreateLink(postDto.Id)
            };
            model = _Mapper.Map<PostModel>(postDto.Result);


            return Ok(model); 
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private PostListModel CreatePostListModel(PostDto post)
        {
            var model = new PostListModel
            {
                Name = post.Name
            };
            model.Url = CreateLink(post.ID);
            return model;
        }

        // not used at the moment
        private PostModel CreatePostModel(PostDto post)
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
