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
        public Post CreatePost(int userId, string body, int score)
        {
            using (var db = new StackoverflowDbContext())
            {
                var newPost = new Post {UserId = userId, Body = body, Score = score };
                newPost.Id = db.Posts.Max(p => p.Id) + 1;
                db.Posts.Add(newPost);
                db.SaveChanges();
                return newPost;
            }
        }

        public List<Post> GetPosts()
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = db.Posts;
                result.Include(x => x.Answers);
                result.Include(x => x.Questions);
                result.Include(x => x.PostTags);
                result.Include(x => x.Links);
                

                return result.ToList();  
            }
        }

        public Post GetPost(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var post = db.Posts
                        .Include(x => x.Answers)
                             .Include(x => x.Questions)
                                .Include(x => x.PostTags)
                                     .Include(x => x.Links)
                                        .FirstOrDefault(x => x.Id == id);
                return post;
            }
        }

        public bool DeletePost(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var post = GetPost(id);
                if (post == null) return false;
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateCategory(int id, string body, int score, DateTime creationDate)
        {
            using (var db = new StackoverflowDbContext())
            {
                var post = GetPost(id);
                if (post == null) return false;
                post.Body = body;
                post.Score = score;
                post.CreationDate = creationDate;
                db.Posts.Update(post);
                db.SaveChanges();
                return true;
            }
        }
    }
}
