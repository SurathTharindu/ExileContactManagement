﻿using System.Collections.Generic;
using ExileContactManagement.Models;
using NHibernate.Criterion;

namespace ExileContactManagement.DBAccess
{
    public class ContactManagement
    {
        UserManagement userMn=new UserManagement();
        public void CreateContact(string username,Contact newContact)
        {
            var user = userMn.GetUserByUsername(username);
            newContact.User = user;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                session.Save(newContact);
                transaction.Commit();
            }
        }

        public void UpdateContact(string username,Contact newContact)
        {
            var user = userMn.GetUserByUsername(username);
            newContact.User = user;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                var contact = session.Load<Contact>(newContact.Id);
                contact.Name = newContact.Name;
                contact.Location = newContact.Location;

                session.Update(contact);
                transaction.Commit();
            }
        }

        public void DeleteContact(int id)
        {
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(session.Get<Contact>(id));
                transaction.Commit();
            }
        }

        public Contact GetContactById(int id)
        {
            Contact contact;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                contact=session.Load<Contact>(id);
                transaction.Commit();
            }
            return contact;
        }


        public IList<Contact> ContactList(string username)
        {
            var user = userMn.GetUserByUsername(username);
            return user.ContactList;
        }

        public List<Contact> SearchedUserContacts(string username, string searchQuery)
        {
            var user = userMn.GetUserByUsername(username);
            List<Contact> searchContacts;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                searchContacts = (List<Contact>)session.QueryOver<Contact>().Where(x => x.Name.IsLike(@"%" + searchQuery + "%") || x.Location.IsLike(@"%" + searchQuery + "%")).And(x => x.User.UId == user.UId).List();
                transaction.Commit();
            }
            return searchContacts;
        }
    }
}