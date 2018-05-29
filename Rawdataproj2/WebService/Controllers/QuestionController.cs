using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataService;
using DataService.Dto.QuestionDto;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using WebService.Models.Note;
using WebService.Models.Question;
using WebService.Models.Tag;
using WebService.Models.User;

namespace WebService.Controllers
{
    [Route("api/questions")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _QuestionRepository;
        private readonly IMapper _Mapper;

        public QuestionController(IQuestionRepository QuestionRepository, IMapper Mapper)
        {
            _QuestionRepository = QuestionRepository;
            _Mapper = Mapper;
        }

        [HttpGet(Name = nameof(GetQuestions))]
        public async Task<IActionResult> GetQuestions(PagingInfo pagingInfo)
        {
            var question = await _QuestionRepository.GetAll(pagingInfo);
            var model = question.Select(que => CreateQuestionListModel(que));

            var total = _QuestionRepository.Count();
            var prev = Url.Link(nameof(GetQuestions), new { page = pagingInfo.Page - 1, pagingInfo.PageSize }).ToLower();
            var next = Url.Link(nameof(GetQuestions), new { page = pagingInfo.Page + 1, pagingInfo.PageSize }).ToLower();

            var returnType = new ReturnTypeConstants("questions");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetQuestion))]
        public async Task<ActionResult> GetQuestion(int id)
        {
            var question = await _QuestionRepository.GetQuestion(id);
            if (question == null) return NotFound();

            var model = _Mapper.Map<QuestionModel>(question);

            return Ok(model);
        } 

        [HttpPut("{id}", Name = nameof(UpdateQuestion))]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionModel model)
        {
            if (model == null || model.ID != id) return BadRequest();

            var q = await _QuestionRepository.Get(id);
            if (q == null) return NotFound();

            q.ID = model.ID;
            q.Title = model.Title;
            q.Body = model.Body;
            //q.ClosedDate = model.ClosedDate;
            q.Score = model.Score;
            q.AcceptedAnswerID = model.AcceptedAnswerID;

            await _QuestionRepository.Update(q);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (!await _QuestionRepository.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionModel model)
        {
            if (model == null) return BadRequest();

            var q = new Question
            {
                Title = model.Title,
                Body = model.Body,
                //UserID = model.UserID,
                Score = model.Score,
                CreationDate = model.Creationdate
            };

            var result = await _QuestionRepository.Add(q);

            return Ok(result);
        }


        /*******************************************************
         * Helpers
         * *****************************************************/
        private QuestionCommentsModel CreateQuestionCommnetsModel(QuestionCommentsDto questionCom)
        {
            var model = new QuestionCommentsModel
            {
                ID = questionCom.ID,
                Score = questionCom.Score,
                Creationdate = questionCom.Creationdate,
                Test = questionCom.Text
            }; 
            return model;
        }

        private QuestionAnswersModel CreateQuestionAnswersModel(QuestionAnswersDto question)
        {
            var model = new QuestionAnswersModel
            {
                 Body = question.Body,
                 Creationdate = question.Creationdate,
                 Score = question.Score
            }; 
            return model;
        }

        private QuestionListModel CreateQuestionListModel(Question question)
        {
            var model = new QuestionListModel
            {
                Title = question.Title,
                Body = question.Body,
                Score = question.Score,
                CreationDate = question.CreationDate,

                User = new UserModel()
                {
                    ID = question.User.ID,
                    DisplayName = question.User.DisplayName,
                },
                PostTags = question.PostTags.Select(tag => new PostTagModel()
                {
                    Tag = tag.Tag,
                }).ToList(),
                //Notes = question.Notes.Select(note => new NoteModel()
                //{ 
                //    Text = note.Text,
                //    Type = note.Post.PostType == 1 ? "Question" : "Answer",
                //    Title = note.Post.Title,
                //    PostID = note.PostID,
                //}).ToList(),

            };
            model.Url = CreateQuestionLink(question.ID);
            return model;
        }
         
        private string CreateQuestionLink(int id)
        {
            return Url.Link(nameof(GetQuestion), new { id });
        }
    

		//[HttpGet("{id}/answers", Name = nameof(GetQuestionAnswers))]
        //public async Task<IActionResult> GetQuestionAnswers(int id)
        //{
        //    var queAnswers = await _QuestionRepository.GetQuestionAnswers(id);
        //    if (queAnswers == null) return NotFound();

        //    var model = queAnswers.Select(que => CreateQuestionAnswersModel(que)); 

        //    return Ok(model);
        //}


        //[HttpGet("{id}/comments", Name = nameof(GetQuestionComments))]
        //public async Task<IActionResult> GetQuestionComments(int id)
        //{
        //    var queCom = await _QuestionRepository.GetQuestionComments(id);
        //    if (queCom == null) return NotFound();

        //    var model = queCom.Select(que => CreateQuestionCommnetsModel(que));

        //    return Ok(model);
        //}
    }
}