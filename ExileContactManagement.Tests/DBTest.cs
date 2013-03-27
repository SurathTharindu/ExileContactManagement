using System.Collections.Generic;
using ExileContactManagement.DBAccess;
using ExileContactManagement.Models;
using FluentAssertions;
using NHibernate;
using NUnit.Framework;

namespace ExileContactManagement.Tests
{
    [TestFixture]
    public class DBTest
    {
        UserManagement UserMn = new UserManagement();
        ContactManagement CntactMn=new ContactManagement();
        private ISession _session;

        [Test]
        public void RegisteredUserExistInDB()
        {
            var user = new User("tomm", "tom123");
            UserMn.RegisterUser(user);

            _session = NhibernateContext.Session;
            List<User> enteredUser;
            using (var transaction = NhibernateContext.Session.BeginTransaction())
            {
                enteredUser=(List<User>) _session.QueryOver<User>()
                                  .Where(x => x.UserName == user.UserName && x.Password == user.Password)
                                  .List();
                transaction.Commit();
            }
            enteredUser.Count.Should().BeGreaterThan(0);
        }

        [Test]
        [Timeout(7500)]
        public void RegisteredUserPerformWell()
        {
            var user = new User("jim", "jim123");
            UserMn.RegisterUser(user);
        }

        [Test]
        public void ChekExistanceOfUsername()
        {
            var user = new User("jerry", "jerry123");
            UserMn.RegisterUser(user);

            var checkedUser = UserMn.GetUserByUsername("jerry");
            checkedUser.Should().NotBeNull();
        }

        [Test]
        public void CheckExistanceOfUserContact()
        {
            var user = new User("nick", "nick123");
            UserMn.RegisterUser(user);

            var contact = new Contact("john", "Colombo");
            CntactMn.CreateContact(user.UserName,contact);

            _session = NhibernateContext.Session;
            List<Contact> addedContact;
            using (var transaction = NhibernateContext.Session.BeginTransaction())
            {
                addedContact = (List<Contact>)_session.QueryOver<Contact>()
                                  .Where(x => x.Id==contact.Id)
                                  .List();
                transaction.Commit();
            }
            addedContact.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void UpdateRecordCorrectly()
        {
            var user = new User("paul", "paul123");
            UserMn.RegisterUser(user);

            var contact = new Contact("jonny", "Kandy");
            CntactMn.CreateContact(user.UserName,contact);

            contact.Name = "Sandy";
            contact.Location = "Kurunegala";
            CntactMn.UpdateContact(user.UserName,contact);

            _session = NhibernateContext.Session;
            List<Contact> addedContact;
            using (var transaction = NhibernateContext.Session.BeginTransaction())
            {
                addedContact = (List<Contact>)_session.QueryOver<Contact>()
                                  .Where(x => x.Id == contact.Id)
                                  .List();
                transaction.Commit();
            }
            addedContact[0].Name.Should().Be(contact.Name);
            addedContact[0].Location.Should().Be(contact.Location);
        }

        [Test]
        public void DeleteRecordCorrectly()
        {
            var user = new User("mike", "mike123");
            UserMn.RegisterUser(user);

            var contact = new Contact("hussey", "Australia");
            CntactMn.CreateContact(user.UserName,contact);

            CntactMn.DeleteContact(contact.Id);

            _session = NhibernateContext.Session;
            List<Contact> addedContact;
            using (var transaction = NhibernateContext.Session.BeginTransaction())
            {
                addedContact = (List<Contact>)_session.QueryOver<Contact>()
                                  .Where(x => x.Id == contact.Id)
                                  .List();
                transaction.Commit();
            }
            addedContact.Count.Should().Be(0);
        }

        [Test]
        public void AuthenticateRegiteredUser()
        {
            var user = new User("mike", "mike123");
            UserMn.RegisterUser(user);

            UserMn.AuthenticateUser("mike", "mike123").Should().BeTrue();
        }

        [Test]
        public void CorrectlyResetPassword()
        {
            var user = new User("hobit", "hobit123");
            UserMn.RegisterUser(user);

            const string newPassword = "123456";
            UserMn.ResetPassword(user.UserName, newPassword);

            var newUser=UserMn.GetUserByUsername(user.UserName);
            newUser.Password.Should().NotBe("hobit123").And.Be(newPassword);
        }

        [Test]
        public void SearchContactsCorrectly()
        {
            var user = new User("harry", "harry123");
            UserMn.RegisterUser(user);

            var contact = new Contact("potter", "England");
            CntactMn.CreateContact(user.UserName, contact);

            var searchList=CntactMn.SearchedUserContacts(user.UserName,"England");
            searchList.Count.Should().BeGreaterThan(0);
        }
    }
}
