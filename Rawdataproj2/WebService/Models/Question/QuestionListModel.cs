using System.Collections.Generic;
using WebService.Models.Tag;

namespace WebService.Models.Question
{
    public class QuestionListModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public IList<PostTagModel> PostTags { get; set; }
    }
}