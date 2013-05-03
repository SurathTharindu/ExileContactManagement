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
        readonly UserManagement _userMn = new UserManagement();
        private readonly ISession _session= NhibernateContext.Session;

        [Test]
        public void RetrievedUserMatchesSavedUser()
        {
            var user = new User("tomm", "tom123");
            _userMn.RegisterUser(user);

            _session.QueryOver<User>().List().Should().Contain(user);
        }

        [Test]
        [Timeout(7500)]
        public void RegistratingUserPerformWell()
        {
            var user = new User("jim", "jim123");
            _userMn.RegisterUser(user);
        }

        [Test]
        public void RetrievedExistingUserByUsername()
        {
            var user = new User("jerry", "jerry123");
            _userMn.RegisterUser(user);

            var checkedUser = _userMn.GetUserByUsername("jerry");
            checkedUser.Should().NotBeNull();
        }

        [Test]
        public void RetrievedRelevantUserByUsername()
        {
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'jerry'").ExecuteUpdate();

            var user = new User("jerry", "jerry123");
            _userMn.RegisterUser(user);

            var checkedUser = _userMn.GetUserByUsername("jerry");
            checkedUser.Should().Be(user);
        }

        [Test]
        public void NotRetrievedNonExistingUserByUsername()
        {
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'snow'").ExecuteUpdate();

            var checkedUser = _userMn.GetUserByUsername("snow");
            checkedUser.Should().BeNull();
        }

        

        [Test]
        public void AuthenticateRegiteredUser()
        {
            var user = new User("mike", "mike123");
            _userMn.RegisterUser(user);

            _userMn.AuthenticateUser("mike", "mike123").Should().BeTrue();
        }

        [Test]
        public void NotAuthenticateUnregiteredUser()
        {
            _session.CreateQuery("DELETE FROM User WHERE UserName = 'neil'").ExecuteUpdate();
            _userMn.AuthenticateUser("neil", "neil123").Should().BeFalse();
        }

        [Test]
        public void CorrectlyResetUserPassword()
        {
            var user = new User("hobit", "hobit123");
            _userMn.RegisterUser(user);

            const string newPassword = "123456";
            _userMn.ResetPassword(user.UserName, newPassword);

            var newUser = _userMn.GetUserByUsername(user.UserName);
            newUser.Password.Should().NotBe("hobit123").And.Be(newPassword);
        }
    }
}
