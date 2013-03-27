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
                //  if (Membership.ValidateUser(model.UserName, model.Password))
                if (userMgr.AuthenticateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    HttpCookie myCookie = new HttpCookie("loginCookie");
                    myCookie.Value = model.UserName;
                    Response.Cookies.Add(myCookie);
                 //   ContactController temp= new ContactController();
                 //   temp.NameOfCurrentUser = model.UserName;          
                    return RedirectToAction("Index", "Contact");

                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /User/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
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
                userMgr = new UserManagement();
                userMgr.RegisterUser(new User() { UserName = userModel.UserName, Password = userModel.Password });
                FormsAuthentication.SetAuthCookie(userModel.UserName, true /* createPersistentCookie */);
                return RedirectToAction("Index", "User");
                // Attempt to register the user
                /*    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie #1#);
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }*/
            }
            // If we got this far, something failed, redisplay form
            return View(userModel);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
                // TODO: Add insert logic here
                // UserManagement userMgr = new UserManagement();
                userMgr.RegisterUser(createdUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
