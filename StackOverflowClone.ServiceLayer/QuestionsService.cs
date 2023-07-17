using AutoMapper;
using StackOverflowClone.DomainModels;
using StackOverflowClone.Repositories;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ServiceLayer
{
    public interface IQuestionService
    {
        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetails(EditQuestionViewModel qvm);
        void UpdateQuestionVotesCount(int questionID, int value);
        void UpdateQuestionAnswersCount(int questionID, int value);
        void UpdateQuestionViewsCount(int questionID, int value);
        void DeleteQuestion(int questionID);
        List<QuestionViewModel> GetQuestions();

        QuestionViewModel GetQuestionByQuestionID(int questionID, int userID);

    }
    public class QuestionsService : IQuestionService
    {
        IQuestionRespository questionRespository;
        public QuestionsService()
        {
            questionRespository = new QuestionsRepository();
        }
        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewQuestionViewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(qvm);
            questionRespository.InsertQuestion(question);
        }

        public void UpdateQuestionAnswersCount(int questionID, int value)
        {
            questionRespository.UpdateQuestionAnswersCount(questionID, value);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditQuestionViewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel,Question>(qvm);
            questionRespository.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionVotesCount(int questionID, int value)
        {
            questionRespository.UpdateQuestionVotesCount(questionID, value);
        }
        public void UpdateQuestionViewsCount(int questionID, int value)
        {
            questionRespository.UpdateQuestionViewsCount(questionID, value);
        }

        public void DeleteQuestion(int questionID)
        {
            questionRespository.DeleteQuestion(questionID);
        }
        public List<QuestionViewModel> GetQuestions()
        {
           List<Question> questions = questionRespository.GetQuestions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User,UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return qvm;
        }

        public QuestionViewModel GetQuestionByQuestionID(int questionID, int userID)
        {
            Question question = questionRespository.GetQuestionByQuestionID(questionID).FirstOrDefault();
            QuestionViewModel qvm = null;
           if (question != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(question);

                foreach(var item in qvm.Answers) {
                    item.CurrentUserVoteType = 0;
                   VoteViewModel vote = item.Votes.Where(temp => temp.UserID == userID).FirstOrDefault();
                   if(vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }

           return qvm;
        }
    }
}
