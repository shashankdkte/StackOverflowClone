using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{
    public interface IQuestionRespository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int questionID, int value);
        void UpdateQuestionAnswersCount(int questionID, int value);
        void UpdateQuestionViewsCount(int questionID, int value);
        void DeleteQuestion(int questionID);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int questionID);


    }
    public class QuestionsRepository : IQuestionRespository
    {
        StackOverflowDbContext db;
        public QuestionsRepository()
        {
            db = new StackOverflowDbContext(); 
        }
        public void DeleteQuestion(int questionID)
        {
            Question deleteQuestion = db.Questions.Where(temp =>temp.QuestionID == questionID).FirstOrDefault();
            if(deleteQuestion != null)
            {
                db.Questions.Remove(deleteQuestion);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestionByQuestionID(int questionID)
        {
            List<Question> questionList = db.Questions.Where(temp =>  temp.QuestionID == questionID).ToList();
            return questionList;
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = db.Questions.ToList();
            return questions;
        }

        public void InsertQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int questionID, int value)
        {
            Question question  = db.Questions.Where(temp => temp.QuestionID==questionID).FirstOrDefault();  
            if(question != null)
            {
                question.AnswersCount += value;
                db.SaveChanges() ;
            }
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question updateQuestion = db.Questions.Where(temp => temp.QuestionID == question.QuestionID).FirstOrDefault();
            if (updateQuestion != null)
            {
                updateQuestion.QuestionName = question.QuestionName;
                updateQuestion.QuestionDateAndTime = question.QuestionDateAndTime;
                updateQuestion.CategoryID = question.CategoryID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int questionID, int value)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (question != null)
            {
                question.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int questionID, int value)
        {
            Question question = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (question != null)
            {
                question.VotesCount += value;
                db.SaveChanges();
            }
        }
    }
}
