using StackOverflowClone.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StackOverflowClone.ApiControllers
{
    public class QuestionsController : ApiController
    {
        IAnswerService answerService;
        public QuestionsController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }
        // GET: Questions
        public void Post(int AnswerID,int UserID,int value)
        {
           this.answerService.UpdateAnswerVotesCount(AnswerID, UserID, value);
        }
    }
}