using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowClone.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService questionService;
        IAnswerService answerService;
        ICategoriesService categoriesService;
        public QuestionsController(IQuestionService questionService, IAnswerService answerService,
            ICategoriesService categoriesService) { 
            this.questionService = questionService;
            this.answerService = answerService;
            this.categoriesService = categoriesService;
        }
        // GET: Questions
        [Route("questions/view/{id}")]
        public ActionResult View(int id)
        {
            this.questionService.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.questionService.GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer(NewAnswerViewModel answer)
        {
            answer.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            answer.AnswerDateAndTime = DateTime.Now;
            answer.VotesCount = 0;
            if(ModelState.IsValid)
            {
                this.answerService.InsertAnswer(answer);
                return RedirectToAction("View","Questions",new {id = answer.QuestionID});
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel qvm = this.questionService.GetQuestionByQuestionID(answer.QuestionID, answer.UserID);
                return View("View",qvm);
            }
        }
      
    }
}