using ExileContactManagement.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ExileContactManagement.DBAccess
{
    public class NhibernateContext
    {
        private ISession _session;

        public NhibernateContext()
        {
            _session = null;
        }

        public ISession Session
        {
            get
            {
                if (_session == null)
                {
                    var sessionFactory = CreateSessionFactory();
                    var session = sessionFactory.OpenSession();
                    _session = session;
                }
                return _session;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(c => c
                            .FromConnectionStringWithKey("Server=localhost; Port=3306; Database=ExileContactMgt; Uid=sa; Pwd=eXile123;")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
              .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
              .BuildSessionFactory();
        }
    }
}