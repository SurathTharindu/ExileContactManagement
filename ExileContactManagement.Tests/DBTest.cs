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
        NhibernateContext nhibernateContext=new NhibernateContext();
        private ISession _session;

        [Test]
        public void RegisteredUserExistInDB()
        {
            _session = nhibernateContext.Session;
            User user = new User("todfdmq", "11111");
            UserMn.RegisterUser(user);

            int userId = 0;
            using (ITransaction transaction = nhibernateContext.Session.BeginTransaction())
            {
                userId=_session.Query<User>()
                     .Where(uic => uic.UserName == user.UserName && uic.Password == user.Password)
                     .Select(uic => uic.UId)
                     .SingleOrDefault();
                transaction.Commit();
            }
            userId.Should().NotBe(0);
        }
    }
}
