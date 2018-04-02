﻿using DataRepository;
using DataRepository.Dto.PostDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class NoteRepository : INoteRepository
    {
        public async void Add(Note note)
        {
            using (var db = new StackoverflowDbContext())
            {
                  await db.Notes.AddAsync(note);
            } 
        }

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return  db.Notes.Count();
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var note = await Get(id);
                if (note==null)
                {
                    return false;
                }
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
                return true;
            } 
        }
         
        public async Task<Note> Get(int userId)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Notes.FirstOrDefaultAsync(x => x.UserID == userId);
            }
        }

        public async Task<IEnumerable<Note>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Notes.ToListAsync();
            }
        }

        public async Task<bool> Update(int userId, Note userNote)
        {
            using (var db = new StackoverflowDbContext())
            {
                var note = await Get(userId);
                if (note == null) return false;
                //TODO: update note attributess
                db.Notes.Update(userNote);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}