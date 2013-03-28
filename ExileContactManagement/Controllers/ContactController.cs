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
                return System.Web.HttpContext.Current.User.Identity.Name;
            }
        }

        //
        // GET: /Contact/
        private ContactManagement contactManager = new ContactManagement();


        public ActionResult Index()
        {

            if (NameOfCurrentUser != null && NameOfCurrentUser != "")
            {
                var contactList = contactManager.ContactList(NameOfCurrentUser);
                if (contactList != null && contactList.Count > 0)
                    return View(contactList);
                return View(new List<Contact>());
            }
            return RedirectToAction("Index","User");
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            if (NameOfCurrentUser != null && NameOfCurrentUser!="")
                return View();
            return RedirectToAction("Index","User");
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contactModel)
        {
            try
            {
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
                contactManager.UpdateContact(NameOfCurrentUser, updatingContact);
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
            if (NameOfCurrentUser != null && NameOfCurrentUser != "")
            {
                Search searchingModel = new Search();
                searchingModel.ResultList = new List<Contact>(contactManager.ContactList(NameOfCurrentUser));
                return View(searchingModel);
            }
            return RedirectToAction("Index","User");
        }

        //POST: /Contact/Search
        [HttpPost]
        public ActionResult Search(Search searchingModel)
        {
            searchingModel.ResultList.Clear();
            searchingModel.ResultList = contactManager.SearchedUserContacts(NameOfCurrentUser, searchingModel.SearchString);
            return View(searchingModel);
        }
    }
}
