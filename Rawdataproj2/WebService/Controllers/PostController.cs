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
            var posts = await _PostRepository
                .GetAll(pagingInfo);
                //.Select(CreatePostListModel);

            IEnumerable<PostListModel> model = posts.Select(post => CreatePostListModel(post));

            return Ok(model);




            //var question = _PostRepository.GetAll().Select(
            //        async q => await new QuestionShortDto
            //        {
            //            QuestionID = q.ID,
            //            Title = q.Title,
            //            Score = q.Score,
            //        //Answers = q.Answers
            //        // AcceptedAnswer = q.AcceptedAnswer
            //    }
            //        ).ToList();



            //return Ok(await _PostRepository.GetAll());
        }


        [HttpGet("{id}", Name = nameof(GetPost))]
        public async Task<ActionResult> GetPost(int id)
        {
            var post = _PostRepository.Get(id);
            if (post == null) return NotFound();

            PostModel model = new PostModel
            {
                Url = CreateLink(post.Id)
            };
            model = _Mapper.Map<PostModel>(post);

            return Ok(model);

            //var question = _dataService.Get(id);
            //if (question == null)
            //{
            //    return null;
            //}
            //QuestionDto model = new QuestionDto
            //{
            //    QuestionID = question.ID,
            //    UserId = question.UserID,
            //    Title = question.Title,
            //    Score = question.Score,
            //    // Body = question.Body,
            //    Creationdate = question.CreationDate,
            //    ClosedDate = question.ClosedDate, 
            //    AcceptedAnswerID = question.AcceptedAnswerID,


            //    Comments = question.Comments
            //            .Select(c => new CommentDto
            //            {
            //                ID = c.ID,
            //                Score = c.Score,
            //                Text = c.Text,
            //                CreationDate = c.CreationDate
            //            }) 
            //        .ToList(),

            //    Answers = question.Answers.Select(x =>
            //    new AnswerDto {
            //        ID = x.ID,
            //        QuestionID = x.ParentID,
            //        Score = x.Score,
            //        Title = x.Body,

            //    }).ToList(), 
            //};
            //return model;


            return Ok(await _PostRepository.Get(id));
        }

        /*******************************************************
         * Helpers
         * *****************************************************/

        private PostListModel CreatePostListModel(Question post)
        {
            var model = new PostListModel
            {
                Name = post.Title
            };
            model.Url = CreateLink(post.ID);
            return model;
        }

        // not used at the moment
        private PostModel CreatePostModel(DomainModel.Question post)
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
