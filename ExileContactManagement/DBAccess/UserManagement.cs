using ExileContactManagement.Models;
using NHibernate;

namespace ExileContactManagement.DBAccess
{
    public class UserManagement
    {
        private NhibernateContext _context;
        public UserManagement()
        {
            _context = new NhibernateContext();
        }

        public void RegisterUser(User newUser)
        {
            ISession session = _context.Session;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }
    }
}