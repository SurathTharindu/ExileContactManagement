using System.Collections.Generic;
using ExileContactManagement.Models;
using NHibernate;

namespace ExileContactManagement.DBAccess
{
    public class ContactManagement
    {
        UserManagement userMn=new UserManagement();
        public void CreateContact(string username,Contact newUser)
        {
            var user = userMn.GetUserByUsername(username);
            user.ContactList.Add(newUser);
            ISession session = NhibernateContext.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public void UpdateContact(Contact newContact)
        {
            ISession session = NhibernateContext.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(newContact);
                transaction.Commit();
            }
        }

        public IList<Contact> ContactList(string username)
        {
            var user = userMn.GetUserByUsername(username);
            return user.ContactList;
        }

        public void DeleteContact(Contact newContact)
        {
            ISession session = NhibernateContext.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(newContact);
                transaction.Commit();
            }
        }
    }
}