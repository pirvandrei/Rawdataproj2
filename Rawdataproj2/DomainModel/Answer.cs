using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Answer
    {
        public int PostId { get; set; }
        public int ParentId { get; set; }

        public Post Post { get; set; }
    }
}
