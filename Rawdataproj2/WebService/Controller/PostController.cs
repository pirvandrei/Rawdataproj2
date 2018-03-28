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

        [HttpGet]
        public IList<QuestionShortDto> Get()
        { 
            var question = _dataService.GetQuestions().Select(
                q => new QuestionShortDto
                {
                    QuestionID = q.ID, 
                    Title = q.Title,
                    Score = q.Score, 
                }
                ).ToList();
           
            return question;
        }


        [HttpGet("{id}")]
        public QuestionDto Get(int id)
        {

            var question = _dataService.GetQuestion(id);
            if (question == null)
            {
                return null;
            }   
            QuestionDto model = new QuestionDto
            {
                QuestionID = question.ID, 
                //UserId = question.UserID, 
                //AcceptedAnswerID = question.AcceptedAnswerID,
                //Title = question.Title,
                Score = question.Score,
                //Body = question.Body,
                //Creationdate = question.CreationDate,
                //ClosedDate = question.ClosedDate,
                //Answers = question.Answers, 
            };
            return model;
        }
    } 
}
