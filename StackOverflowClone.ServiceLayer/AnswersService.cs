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
    public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void UpdateAnswerVotesCount(int answerID, int userID, int value);
        void DeleteAnswer(int answerID);
        List<AnswerViewModel> GetAnswersByQuestionID(int questionID);
        AnswerViewModel GetAnswerByAnswerID(int answerID);
    }
    public class AnswersService : IAnswerService
    {
        IAnswersRepository answersRepository;
        public AnswersService()
        {
            answersRepository = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            answersRepository.InsertAnswer(a);
        }
        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            answersRepository.UpdateAnswer(a);
        }
        public void UpdateAnswerVotesCount(int answerID, int userID, int value)
        {
            answersRepository.UpdateAnswerVotesCount(answerID, userID, value);
        }
        public void DeleteAnswer(int answerID)
        {
            answersRepository.DeleteAnswer(answerID);
        }
        public List<AnswerViewModel> GetAnswersByQuestionID(int qid)
        {
            List<Answer> a = answersRepository.GetAnswersByQuestionID(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return avm;
        }

        public AnswerViewModel GetAnswerByAnswerID(int AnswerID)
        {
            Answer a = answersRepository.GetAnswersByAnswerID(AnswerID).FirstOrDefault();
            AnswerViewModel avm = null;
            if (a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }
            return avm;
        }

       
    }
}
