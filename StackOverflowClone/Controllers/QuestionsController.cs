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
        
        public ActionResult EditAnswer(EditAnswerViewModel answer)
        {
            if(ModelState.IsValid)
            {
                answer.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.answerService.UpdateAnswer(answer);
                return RedirectToAction("View", new {id = answer.QuestionID});
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return RedirectToAction("View",new {id = answer.QuestionID});
            }
        }

        public ActionResult Create()
        {
            List<CategoryViewModel> categories = this.categoriesService.GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewQuestionViewModel question)
        {
            if(ModelState.IsValid)
            {
                question.AnswersCount = 0;
                question.ViewsCount = 0;
                question.VotesCount = 0;
                question.QuestionDateAndTime = DateTime.Now;
                question.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.questionService.InsertQuestion(question);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}