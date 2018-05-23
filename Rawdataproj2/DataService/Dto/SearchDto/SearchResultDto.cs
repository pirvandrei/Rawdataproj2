﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Dto.SearchDto
{
    public class SearchResultDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostType { get; set; }
        public DateTime CreationDate { get; set; }
        public int AcceptedAnswerId { get; set; }
        public decimal Rank { get; set; }
        public string Body { get; set; }
    }
}