using System.Collections.Generic;
using ExileContactManagement.DBAccess;
using ExileContactManagement.Models;
using FluentAssertions;
using NHibernate;
using NUnit.Framework;

namespace ExileContactManagement.Tests
{
    [TestFixture]
    public class ContactManagementTest 
    {
        UserManagement UserMn = new UserManagement();
        ContactManagement CntactMn = new ContactManagement();
        private ISession _session;

        [Test]
        public void SavedContactsCanBeRetrieved()
        {
            var user = new User("nick", "nick123");
            UserMn.RegisterUser(user);

            var contact = new Contact("john", "Colombo");
            CntactMn.CreateContact(user.UserName, contact);

            _session = NhibernateContext.Session;
            ((List<Contact>)_session.QueryOver<Contact>().List()).Should().Contain(contact);
        }

        [Test]
        public void ContactNameUpdatedCorrectly()
        {
            var user = new User("paul", "paul123");
            UserMn.RegisterUser(user);

            var contact = new Contact("jonny", "Kandy");
            CntactMn.CreateContact(user.UserName, contact);

            contact.Name = "Sandy";
            CntactMn.UpdateContact(user.UserName, contact);

            _session = NhibernateContext.Session;
            var addedContact = _session.QueryOver<Contact>()
                                                   .Where(x => x.Id == contact.Id)
                                                   .SingleOrDefault();

            addedContact.Name.Should().Be(contact.Name);
        }

        [Test]
        public void ContactLocationUpdatedCorrectly()
        {
            var user = new User("saman", "sam123");
            UserMn.RegisterUser(user);

            var contact = new Contact("rox", "Polgahawela");
            CntactMn.CreateContact(user.UserName, contact);

            contact.Location = "Galle";
            CntactMn.UpdateContact(user.UserName, contact);

            _session = NhibernateContext.Session;
            var addedContact = _session.QueryOver<Contact>()
                                                   .Where(x => x.Id == contact.Id)
                                                   .SingleOrDefault();

            addedContact.Location.Should().Be(contact.Location);
        }

        [Test]
        public void DeleteExistingContactCorrectly()
        {
            var user = new User("mike", "mike123");
            UserMn.RegisterUser(user);

            var contact = new Contact("hussey", "Australia");
            CntactMn.CreateContact(user.UserName, contact);

            CntactMn.DeleteContact(contact.Id);

            _session = NhibernateContext.Session;
            ((List<Contact>)_session.QueryOver<Contact>().List()).Should().NotContain(contact);
        }

        [Test]
        public void ShouldFindMatchingContacts()
        {
            var user = new User("harry", "harry123");
            UserMn.RegisterUser(user);

            var contact = new Contact("potter", "England");
            CntactMn.CreateContact(user.UserName, contact);

            var searchList = CntactMn.SearchedUserContacts(user.UserName, "England");
            searchList.Count.Should().BeGreaterThan(0);
        }
    }
}
