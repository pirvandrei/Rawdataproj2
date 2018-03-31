using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using DataRepository.Dto.PostDto;
using DataRepository;

namespace WebService.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository _PostRepository; 

        public PostController(IPostRepository PostRepository)
        {
            _PostRepository = PostRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            // TODO: return SearchHistoryListModel instead.

            
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

               

            return Ok(await _PostRepository.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {

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
    } 
}
