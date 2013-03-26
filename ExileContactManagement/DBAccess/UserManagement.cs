using System.Collections.Generic;
using ExileContactManagement.Models;

namespace ExileContactManagement.DBAccess
{
    public class UserManagement
    {

        public void RegisterUser(User newUser)
        {
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public void ResetPassword(string username, string newPassword)
        {
            var user = GetUserByUsername(username);
            user.Password = newPassword;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(user);
                transaction.Commit();
            }
        }

        public User GetUserByUsername(string username)
        {
            List<User> enteredUser;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                enteredUser = (List<User>)session.QueryOver<User>()
                                               .Where(uic => uic.UserName == username)
                                               .List();
                transaction.Commit();
            }
            return enteredUser.Count >= 1 ? enteredUser[0] : null;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var authenticate = false;
            List<User> authenticatedList;
            var session = NhibernateContext.Session;
            using (var transaction = session.BeginTransaction())
            {
                authenticatedList = (List<User>)session.QueryOver<User>()
                                               .Where(x => x.UserName == username && x.Password==password)
                                               .List();
                transaction.Commit();
            }
            if (authenticatedList.Count >= 1)
            {
                authenticate=true;
            }
            return authenticate;
        }
    }
}