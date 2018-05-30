using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Dto.PostDto;
using DataService;

using AutoMapper;
using WebService.Models.Note;
using WebService.Models;
using WebService.Models.User;
using Microsoft.Extensions.Logging;
using DataService.Dto.NoteDto;

namespace WebService.Controllers
{
    [Route("api/notes")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _NoteRepository;
        private readonly IMapper _Mapper;
        //private readonly ILogger _logger;


        public NoteController(INoteRepository NoteRepository, IMapper Mapper/*, ILogger<NoteController> logger*/)
        {
            _NoteRepository = NoteRepository;
            _Mapper = Mapper;
            //_logger = logger;
        }

        [HttpGet(Name = nameof(GetNotes))]
        public async Task<IActionResult> GetNotes(PagingInfo pagingInfo)
        {
            
           // var notes = await _NoteRepository.GetAll(pagingInfo);
			var notes = await _NoteRepository.GetNotes(pagingInfo); 
            var model = notes.Select(note => CreateNoteListModel(note));

            var total = _NoteRepository.Count();
            var prev = Url.Link(nameof(GetNotes), new { page = pagingInfo.Page - 1, pagingInfo.PageSize }).ToLower();
            var next = Url.Link(nameof(GetNotes), new { page = pagingInfo.Page + 1, pagingInfo.PageSize }).ToLower();

            var returnType = new ReturnTypeConstants("notes");
            var result = PagingHelper.GetPagingResult(pagingInfo, total, model, returnType, prev, next);

            return Ok(result);
        }


        [HttpGet("{id}", Name = nameof(GetNote))]
        public async Task<IActionResult> GetNote(int id)
        {
            //_logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", id);
            var note = await _NoteRepository.Get(id);
            if (note == null)
            {
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            var model = CreateNoteModel(note);

            return Ok(model);
        }

        [HttpPut("{id}", Name = nameof(UpdateNote))]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteModel updateNote)
        {
            if (updateNote == null || updateNote.PostID != id) return BadRequest("Id's do not match");

            //var user = new User { ID = 1, };
            var note = await _NoteRepository.Get(id);
            if (note == null) return NotFound();

            note.Text = updateNote.Text;
            note.UserID = updateNote.UserID;
            note.PostID = updateNote.PostID;           
            var result = await _NoteRepository.Update(note);
            if (result) { return Ok(); } else { BadRequest("something went wrong"); }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (!await _NoteRepository.Delete(id)) return NotFound();
            return Json(NoContent());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteModel model)
        {
            if (model == null) return BadRequest();          
            var note = new Note
            {
                Text = model.Text,
                PostID = model.PostId,
                UserID = model.UserId, 
            };          
            var result = await _NoteRepository.Add(note);          
            return Json(Ok(result));
        }


        /*******************************************************
         * Helpers
         * *****************************************************/

        private NoteListModel CreateNoteListModel(NoteDto note)
        {
            var model = new NoteListModel
            { 
                Text = note.Text,
                Type = note.Posttype,
                Title = note.Title,
                PostID = note.PostID,
                ParentID = note.ParentID, 
            };
            model.Url = CreateLink(note.PostID);
            return model;
        }

        private NoteModel CreateNoteModel(Note note)
        {
            var model = _Mapper.Map<NoteModel>(note);
            model.Url = CreateLink(note.PostID);
            model.Title = note.Post.Title;
            model.Type = note.Post.PostType == 1 ? "Question": "Answer";
            return model;
        }


        private string CreateLink(int id)
        {
            return Url.Link(nameof(GetNote), new { id });
        }

        public class LoggingEvents
        {
            public const int GenerateItems = 1000;
            public const int ListItems = 1001;
            public const int GetItem = 1002;
            public const int InsertItem = 1003;
            public const int UpdateItem = 1004;
            public const int DeleteItem = 1005;          
            public const int GetItemNotFound = 4000;
            public const int UpdateItemNotFound = 4001;
        }
    }
}
