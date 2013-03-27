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
    public class ContactController : Controller
    {

        public string NameOfCurrentUser
        {
            get
            {
                HttpCookie loginCookie = Request.Cookies["loginCookie"];
                return Server.HtmlEncode(loginCookie.Value);
            }
        }
        //
        // GET: /Contact/

        private ContactManagement contactManager= new ContactManagement();
        

        public ActionResult Index()
        {

            HttpCookie myCookie = new HttpCookie("MyTestCookie");
            myCookie = Request.Cookies["MyTestCookie"];

            // Read the cookie information and display it.
            if (myCookie != null)
                Response.Write("<p>" + myCookie.Name + "<p>" + myCookie.Value);
            else
                Response.Write("not found");

            var contactList = contactManager.ContactList(NameOfCurrentUser);
            if(contactList!=null && contactList.Count>0)
                return View(contactList);
            return View();
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contactModel)
        {
            try
            {
                // TODO: Add insert logic here
                contactManager.CreateContact(NameOfCurrentUser, contactModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            var selectedContact = contactManager.GetContactById(id);
            return View(selectedContact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Contact updatingContact)
        {
            try
            {
                contactManager.UpdateContact(NameOfCurrentUser,updatingContact);   
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            var selectedContact = contactManager.GetContactById(id);
            return View(selectedContact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Contact deletingContact)
        {
            try
            {
                // TODO: Add delete logic here
                contactManager.DeleteContact(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: /Contact/Search
        public ActionResult Search()
        {
            Search searchingModel = new Search();
           // List<Contact> contacts=(List<Contact>)contactManager.ContactList(NameOfCurrentUser);
            searchingModel.ResultList = new List<Contact>(contactManager.ContactList(NameOfCurrentUser));
            return View(searchingModel);
        }

        //POST: /Contact/Search
        [HttpPost]
        public ActionResult Search(Search searchingModel)
        {
            searchingModel.ResultList= new List<Contact>();

            return View();
        }
    }
}
