using AutoMapper;
using Microsoft.Win32;
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
    public interface IUserService
    {
        int InsertUser(RegisterViewModel registerView);
        void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsView);
        void UpdateUserPassword(EditUserPasswordViewModel editUserPasswordView);
        void DeleteUser(int userID);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string email, string password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);

    }
    public class UsersService : IUserService
    {
        IUsersRepository usersRepository;
        public UsersService()
        {
            usersRepository = new UsersRepository();
        }
        public void DeleteUser(int userID)
        {
            usersRepository.DeleteUser(userID);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = usersRepository.GetUsers();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User,UserViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> usersView = mapper.Map<List<User>, List<UserViewModel>>(users);
            return usersView;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            User user = usersRepository.GetUsersByEmail(Email).FirstOrDefault();
            UserViewModel uvm = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);
            }
            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPassword(string email, string password)
        {
            User user = usersRepository.GetUsersByEmailAndPassword(email,SHA256HashGenerator.GenerateHash(password)).FirstOrDefault();
            UserViewModel uvm = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);
            }
            return uvm;
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            User user = usersRepository.GetUsersByUserID(UserID).FirstOrDefault();
            UserViewModel uvm = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);
            }
            return uvm;
        }

        public int InsertUser(RegisterViewModel registerView)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(registerView);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(registerView.Password);
            usersRepository.InsertUser(user);
            int userID = usersRepository.GetLatestUserID();
            return userID;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsView)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserDetailsViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(editUserDetailsView);
            usersRepository.UpdateUserDetails(user);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel editUserPasswordView)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserPasswordViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(editUserPasswordView);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(editUserPasswordView.Password);
            usersRepository.UpdateUserPassword(user);
        }
    }
}
