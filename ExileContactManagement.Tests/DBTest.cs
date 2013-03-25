using System.Collections.Generic;
using System.Linq;
using ExileContactManagement.DBAccess;
using ExileContactManagement.Models;
using FluentAssertions;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace ExileContactManagement.Tests
{
    [TestFixture]
    public class DBTest
    {
        UserManagement UserMn = new UserManagement();
        private ISession _session;

        [Test]
        public void RegisteredUserExistInDB()
        {
            _session = NhibernateContext.Session;
            var user = new User("tomm", "tom123");
            UserMn.RegisterUser(user);

            List<User> enteredUser;
            using (ITransaction transaction = NhibernateContext.Session.BeginTransaction())
            {
                enteredUser=(List<User>) _session.QueryOver<User>()
                                  .Where(uic => uic.UserName == user.UserName && uic.Password == user.Password)
                                  .List();
                transaction.Commit();
            }
            enteredUser.Count.Should().BeGreaterThan(0);
        }

        [Test]
        [Timeout(7500)]
        public void RegisteredUserPerformWell()
        {
            _session = NhibernateContext.Session;
            var user = new User("tomm", "tom123");
            UserMn.RegisterUser(user);
        }

        [Test]
        public void ChekExistanceOfUsername()
        {
            _session = NhibernateContext.Session;
            var user = new User("tomm", "tom123");
            UserMn.RegisterUser(user);

            var checkedUser=UserMn.GetUserByUsername("tomm");
            checkedUser.Should().NotBeNull();
        }
    }
}
