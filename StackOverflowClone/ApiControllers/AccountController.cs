using StackOverflowClone.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StackOverflowClone.ApiControllers
{
    public class AccountController : ApiController
    {
        IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: Account
        public string Get(string Email)
        {
            if(this.userService.GetUsersByEmail(Email) !=null)

            {
                return "Found";
            }
            else
            {
                return "NotFound";
            }
        }
    }
}