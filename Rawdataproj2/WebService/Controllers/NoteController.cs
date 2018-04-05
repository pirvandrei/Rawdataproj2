﻿using DomainModel;
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
using WebService.Models.Note;

namespace WebService.Controllers
{
    [Route("api/notes")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _NoteRepository;
        private readonly IMapper _Mapper;


        public NoteController(INoteRepository NoteRepository, IMapper Mapper)
        {
            _NoteRepository = NoteRepository;
            _Mapper = Mapper;
        }

        [HttpGet(Name = nameof(GetNotes))]
        public async Task<IActionResult> GetNotes(PagingInfo pagingInfo)
        {
            var notes = await _NoteRepository.GetAll(pagingInfo);
            IEnumerable<NoteListModel> model = notes.Select(note => CreateNoteListModel(note));

            var total = _NoteRepository.Count();
            var pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);

            var prev = pagingInfo.Page > 0
                ? Url.Link(nameof(GetNotes),
                    new { page = pagingInfo.Page - 1, pagingInfo.PageSize })
                : null;

            var next = pagingInfo.Page < pages - 1
                ? Url.Link(nameof(GetNotes),
                    new { page = pagingInfo.Page + 1, pagingInfo.PageSize })
                : null;

            var result = new
            {
                Prev = prev,
                Next = next,
                Total = total,
                Pages = pages,
                Notes = model
            };

            return Ok(result);
        }


        [HttpGet("{id}", Name = nameof(GetNote))]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _NoteRepository.Get(id);
            if (note == null) return NotFound();

            var model = CreateNoteModel(note);

            return Ok(model);
        }

        [HttpPut("{id}", Name = nameof(UpdateNote))]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteModel updateNote)
        {
            if (updateNote == null || updateNote.PostID != id) return BadRequest();

            var user = new User { ID = 1, };
            var note = await _NoteRepository.Get(id);
            if (note == null) return NotFound();

            note.Text = updateNote.Text;
            note.PostID = updateNote.PostID;
            note.UserID = updateNote.UserID;
            await _NoteRepository.Update(user.ID, note);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (! await _NoteRepository.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteModel model)
        {
            if (model == null) return BadRequest();

            var note = new Note
            {
                Text = model.Text,
                PostID = model.PostId,
                UserID = model.UserId
            };

              var result = await _NoteRepository.Add(note);

            return Ok(result);
        }


        /*******************************************************
         * Helpers
         * *****************************************************/

        private NoteListModel CreateNoteListModel(Note note)
        {
            var model = new NoteListModel
            {
                Text = note.Text
            };
            model.Url = CreateLink(note.PostID);
            return model;
        }

        private NoteModel CreateNoteModel(Note note)
        {
            var model = _Mapper.Map<NoteModel>(note);
            //model.Url = CreateLink(note.UserID);
            return model;
        }


        private string CreateLink(int id)
        {
            return Url.Link(nameof(GetNote), new { id });
        }

    }
}
