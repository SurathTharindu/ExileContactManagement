using System.Collections.Generic;
using ExileContactManagement.DBAccess;
using ExileContactManagement.Models;
using FluentAssertions;
using NHibernate;
using NUnit.Framework;

namespace ExileContactManagement.Tests
{
    [TestFixture]
    public class UserManagementTest 
    {
        UserManagement UserMn = new UserManagement();
        ContactManagement CntactMn = new ContactManagement();
        private ISession _session;

        [Test]
        public void RetrievedUserMatchesSavedUser()
        {
            var user = new User("tomm", "tom123");
            UserMn.RegisterUser(user);

            _session = NhibernateContext.Session;
            ((List<User>)_session.QueryOver<User>().List()).Should().Contain(user);
        }

        [Test]
        [Timeout(7500)]
        public void RegistratingUserPerformWell()
        {
            var user = new User("jim", "jim123");
            UserMn.RegisterUser(user);
        }

        [Test]
        public void RetrievedExistingUserByUsername()
        {
            var user = new User("jerry", "jerry123");
            UserMn.RegisterUser(user);

            var checkedUser = UserMn.GetUserByUsername("jerry");
            checkedUser.Should().NotBeNull();
        }

        [Test]
        public void RetrievedRelevantUserByUsername()
        {
            _session = NhibernateContext.Session;
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'jerry'").ExecuteUpdate();

            var user = new User("jerry", "jerry123");
            UserMn.RegisterUser(user);

            var checkedUser = UserMn.GetUserByUsername("jerry");
            checkedUser.Should().Be(user);
        }

        [Test]
        public void NotRetrievedNonExistingUserByUsername()
        {
            _session = NhibernateContext.Session;
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'snow'").ExecuteUpdate();

            var checkedUser = UserMn.GetUserByUsername("snow");
            checkedUser.Should().BeNull();
        }

        

        [Test]
        public void AuthenticateRegiteredUser()
        {
            var user = new User("mike", "mike123");
            UserMn.RegisterUser(user);

            UserMn.AuthenticateUser("mike", "mike123").Should().BeTrue();
        }

        [Test]
        public void NotAuthenticateUnregiteredUser()
        {
            _session = NhibernateContext.Session;
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'neil'").ExecuteUpdate();
            UserMn.AuthenticateUser("neil", "neil123").Should().BeFalse();
        }

        [Test]
        public void CorrectlyResetUserPassword()
        {
            var user = new User("hobit", "hobit123");
            UserMn.RegisterUser(user);

            const string newPassword = "123456";
            UserMn.ResetPassword(user.UserName, newPassword);

            var newUser = UserMn.GetUserByUsername(user.UserName);
            newUser.Password.Should().NotBe("hobit123").And.Be(newPassword);
        }
    }
}
