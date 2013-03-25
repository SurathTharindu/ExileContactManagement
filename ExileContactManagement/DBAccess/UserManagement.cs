using System.Collections.Generic;
using ExileContactManagement.Models;
using NHibernate;

namespace ExileContactManagement.DBAccess
{
    public class UserManagement
    {
        public void RegisterUser(User newUser)
        {
            ISession session = NhibernateContext.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public User GetUserByUsername(string username)
        {
            List<User> enteredUser;
            ISession session = NhibernateContext.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                enteredUser = (List<User>)session.QueryOver<User>()
                                  .Where(uic => uic.UserName == username)
                                  .List();
                transaction.Commit();
            }
            return enteredUser[0];
        }
    }
}