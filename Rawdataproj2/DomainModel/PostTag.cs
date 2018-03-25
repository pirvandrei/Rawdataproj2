using System.Collections.Generic;

namespace DomainModel
{
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public IList<Tag> Tags { get; set; }

    }
}