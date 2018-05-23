using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Dto.SearchDto
{
    public class SearchDto
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int ID { get; set; }
        public User User { get; set; }
    }
}
