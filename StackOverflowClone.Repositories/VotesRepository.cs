using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int answerID, int userID, int value);

    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowDbContext db;
        public VotesRepository()
        {
            db = new StackOverflowDbContext();
        }
        public void UpdateVote(int answerID, int userID, int value)
        {
            int updateValue;
            if(value > 0)
            {
                updateValue = 1;
            }
            else if(value < 0)
            {
                updateValue = -1;
            }
            else
            {
                updateValue = 0;
            }
            Vote vote = db.Votes.Where(temp => temp.AnswerID == answerID && temp.UserID == userID).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote()
                {
                    AnswerID = answerID,
                    UserID = userID,
                    VoteValue = updateValue
                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }
    }
}
