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
        readonly UserManagement _userMn = new UserManagement();
        readonly ContactManagement _cntactMn = new ContactManagement();
        private readonly ISession _session = NhibernateContext.Session;
        private User _user;

        [SetUp]
        public void Initialize()
        {
            _user = new User("nick", "nick123");
            _userMn.RegisterUser(_user); 
        }
        [Test]
        public void SavedContactsCanBeRetrieved()
        {
            var contact = new Contact("john", "Colombo");
            _cntactMn.CreateContact(_user.UserName, contact);

            _session.QueryOver<Contact>().List().Should().Contain(contact);
        }

        [Test]
        public void ContactNameUpdatesAreSaved()
        {
            var contact = new Contact("jonny", "Kandy");
            _cntactMn.CreateContact(_user.UserName, contact);

            contact.Name = "Sandy";
            _cntactMn.UpdateContact(_user.UserName, contact);

            var addedContact = _session.QueryOver<Contact>()
                                                   .Where(x => x.Id == contact.Id)
                                                   .SingleOrDefault();

            addedContact.Should().Be(contact);
        }

        [Test]
        public void ContactLocationUpdatesAreSaved()
        {
            var contact = new Contact("rox", "Polgahawela");
            _cntactMn.CreateContact(_user.UserName, contact);

            contact.Location = "Galle";
            _cntactMn.UpdateContact(_user.UserName, contact);

            var addedContact = _session.QueryOver<Contact>()
                                                   .Where(x => x.Id == contact.Id)
                                                   .SingleOrDefault();

            addedContact.Location.Should().Be(contact.Location);
        }

        [Test]
        public void DeletedContactsAreRemoved()
        {
            var contact = new Contact("hussey", "Australia");
            _cntactMn.CreateContact(_user.UserName, contact);

            _cntactMn.DeleteContact(contact.Id);

            _session.QueryOver<Contact>().List().Should().NotContain(contact);
        }

        [Test]
        public void ShouldFindMatchingContacts()
        {
            var contact = new Contact("potter", "England");
            _cntactMn.CreateContact(_user.UserName, contact);

            var searchList = _cntactMn.SearchedUserContacts(_user.UserName, "England");
            searchList.Count.Should().BeGreaterThan(0);
        }
    }
}
