using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Answer
    {
        
        public int ParentId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
