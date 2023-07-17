using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{

    public interface IUsersRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int userID);

        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string email, string passwordHash);
        List<User> GetUsersByEmail(string email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDbContext();
        }
        public void DeleteUser(int userID)
        {
            User deleteUser = db.Users.Where(temp => temp.UserID == userID).FirstOrDefault();
            if(deleteUser != null)
            {
                db.Users.Remove(deleteUser);
                db.SaveChanges();
            }
        }

        public int GetLatestUserID()
        {
            int uid = db.Users.Select(temp => temp.UserID).Max();
            return uid;
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
            return users;
        }

        public List<User> GetUsersByEmail(string email)
        {
            List<User> users = db.Users.Where(temp => temp.Email == email).ToList();
            return users;
        }

        public List<User> GetUsersByEmailAndPassword(string email, string passwordHash)
        {
            List<User> users = db.Users.Where(temp => temp.Email == email && temp.PasswordHash == passwordHash).ToList();
            return users;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> users = db.Users.Where(temp =>temp.UserID == UserID).ToList();
            return users;
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User updateUser = db.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if(updateUser != null)
            {
                updateUser.Name = user.Name;
                updateUser.Email = user.Email;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User user)
        {
            User updateUser = db.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if (updateUser != null)
            {
                updateUser.PasswordHash = user.PasswordHash;
                db.SaveChanges();
            }
        }
    }
}
