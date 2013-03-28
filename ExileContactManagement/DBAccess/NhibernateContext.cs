using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ExileContactManagement.DBAccess
{
    public class NhibernateContext
    {
        private static ISession _session;

        public static ISession Session
        {
            get
            {
                if (_session == null)
                {
                    var session = CreateSessionFactory().OpenSession();
                    _session = session;
                }
                return _session;
            }
        }

        public static void CloseCookie()
        {
            _session.Flush();
            _session.Close();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            string server = System.Configuration.ConfigurationManager.ConnectionStrings["server"].ToString();
            string database = System.Configuration.ConfigurationManager.ConnectionStrings["database"].ToString();
            string username = System.Configuration.ConfigurationManager.ConnectionStrings["username"].ToString();
            string password = System.Configuration.ConfigurationManager.ConnectionStrings["password"].ToString(); 
            return Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(c => c
                            .Server(server)
                            .Database(database)
                            .Username(username)
                            .Password(password)))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NhibernateContext>())
              .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
              .BuildSessionFactory();
        }
    }
}