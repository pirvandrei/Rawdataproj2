using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using DataRepository.Dto.Post;

namespace WebService.Controller
{
    [Route("api/posts")]
    public class PostController
    {
        StackoverflowDbDataservice _dataService = new StackoverflowDbDataservice();
           
        [HttpGet("{id}")]
        public QuestionDto Get(int id)
        {
            var question = _dataService.GetQuestion(id);
            QuestionDto model = new QuestionDto
            {
                //QuestionID = question.ID,
                Creationdate = question.CreationDate,
                Score = question.Score,
                //AcceptedanswerID = question.AcceptedanswerID
            };
            return model;
        }
    } 
}
