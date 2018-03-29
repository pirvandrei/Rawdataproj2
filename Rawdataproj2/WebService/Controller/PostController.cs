using DomainModel;
using Microsoft.AspNetCore.Mvc;
using StackoverflowContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using DataRepository.Dto.PostDto;

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
                    //Answers = q.Answers
                   // AcceptedAnswer = q.AcceptedAnswer
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
                UserId = question.UserID,
                Title = question.Title,
                Score = question.Score,
                // Body = question.Body,
                Creationdate = question.CreationDate,
                ClosedDate = question.ClosedDate, 
                AcceptedAnswerID = question.AcceptedAnswerID,


                Comments = question.Comments
                        .Select(c => new CommentDto
                        {
                            ID = c.ID,
                            Score = c.Score,
                            Text = c.Text,
                            CreationDate = c.CreationDate
                        }) 
                    .ToList(),

                Answers = question.Answers.Select(x =>
                new AnswerDto {
                    ID = x.ID,
                    QuestionID = x.ParentID,
                    Score = x.Score,
                    Title = x.Body,
                    
                }).ToList(), 
            };
            return model;
        }
    } 
}
