using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowClone.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if(ModelState.IsValid)
            {
                int uid = this._userService.InsertUser(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }
        }


        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this._userService.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if(uvm!=null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.isAdmin;
                    if(uvm.isAdmin)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "Index" });
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Data");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeProfile()
        {

            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this._userService.GetUsersByUserID(uid);
            EditUserDetailsViewModel edvm = new EditUserDetailsViewModel()
            {
                Name = uvm.Name,
                Email = uvm.Email,
                Mobile = uvm.Mobile,
                UserId = uid,
            };
            return View(edvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(EditUserDetailsViewModel edvm)
        {
            if(ModelState.IsValid)
            {
                edvm.UserId = Convert.ToInt32(Session["CurrentUserID"]);
                this._userService.UpdateUserDetails(edvm);
                Session["CurrentUserName"] = edvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(edvm);
            }
        }
    }
}