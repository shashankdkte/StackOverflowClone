using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowClone.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService questionService;
        ICategoriesService categoriesService;
        public HomeController(IQuestionService questionService,ICategoriesService categoriesService)
        {
            this.questionService = questionService;
            this.categoriesService = categoriesService;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<QuestionViewModel> questions = questionService.GetQuestions().Take(10).ToList();
            return View(questions);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<CategoryViewModel> categories = this.categoriesService.GetCategories();
            return View(categories);
        }
    }
}