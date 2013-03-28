using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExileContactManagement.DBAccess;
using ExileContactManagement.Models;

namespace ExileContactManagement.Controllers
{
    public class UserController : Controller
    {
        private UserManagement userMgr = new UserManagement();
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        //Get: /User/LogOn/
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = userMgr.GetUserByUsername(model.UserName);
                if (userMgr.AuthenticateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);      
                    return RedirectToAction("Index", "Contact");

                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        //
        // GET: /User/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }

        // GET: /User/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                userMgr.RegisterUser(new User() { UserName = userModel.UserName, Password = userModel.Password });
                FormsAuthentication.SetAuthCookie(userModel.UserName, true /* createPersistentCookie */);
                return RedirectToAction("Index", "User");
            }
            // If we got this far, something failed, redisplay form
            return View(userModel);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User createdUser)
        {
            try
            {
                userMgr.RegisterUser(createdUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
