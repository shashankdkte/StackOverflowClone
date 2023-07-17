using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int answerID, int userID, int value);
        void DeleteAnswer(int answerID);
        List<Answer> GetAnswersByQuestionID(int questionID);
        List<Answer> GetAnswersByAnswerID(int answerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDbContext db;
        IQuestionRespository questionRespository;
        IVotesRepository votesRepository;
        public AnswersRepository() { 
        db = new StackOverflowDbContext();
        questionRespository = new QuestionsRepository();
        votesRepository   = new VotesRepository();
        }
        public void DeleteAnswer(int answerID)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == answerID).FirstOrDefault();
            if(answer!=null)
            {
                db.Answers.Remove(answer);
                db.SaveChanges();
                questionRespository.UpdateQuestionAnswersCount(answer.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int answerID)
        {
            List<Answer> answers = db.Answers.Where(temp => temp.AnswerID==answerID).ToList();
            return answers;
        }

        public List<Answer> GetAnswersByQuestionID(int questionID)
        {
            List<Answer> answers = db.Answers.Where(temp => temp.QuestionID == questionID).OrderByDescending(temp => temp.AnswerDate).ToList();
            return answers;
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            questionRespository.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }

        public void UpdateAnswer(Answer a)
        {
          Answer answer = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if(answer!=null)
            {
                answer.AnswerText = a.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int answerID, int userID, int value)
        {
            Answer answer = db.Answers.Where(temp =>temp.AnswerID == answerID).FirstOrDefault();
            if(answer!=null)
            {
                answer.VotesCount += value;
                db.SaveChanges();
                questionRespository.UpdateQuestionVotesCount(answer.QuestionID, value);
               // votesRepository.UpdateVote(answerID, userID, value);
            }
        }
    }
}
