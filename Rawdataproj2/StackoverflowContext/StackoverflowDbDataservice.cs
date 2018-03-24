using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackoverflowContext
{
    public class StackoverflowDbDataservice
    {
        public List<Post> GetPosts()
        {
            using (var db = new StackoverflowDbContext())
            {
                return db.Posts.ToList();
            }
        }

    }
}
