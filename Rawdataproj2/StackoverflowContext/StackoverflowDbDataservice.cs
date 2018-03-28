using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackoverflowContext
{
    public class StackoverflowDbDataservice
    {
        //public Post CreatePost(int userId, string body, int score)
        //{
        //    using (var db = new StackoverflowDbContext())
        //    {
        //        var newPost = new Post {UserID = userId,   Score = score };
        //        newPost.ID = db.Posts.Max(p => p.ID) + 1;
        //        db.Posts.Add(newPost);
        //        db.SaveChanges();
        //        return newPost;
        //    }
        //}

        public List<Question> GetQuestions()
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = db.Questions; 
                result.Include(x => x.PostTags);
                result.Include(x => x.Links);
                

                return result.ToList();  
            }
        }

        public Question GetQuestion(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var question = db.Questions
                             .FirstOrDefault(x => x.ID == id);
                return question;
            }
        }


         
    }
}
