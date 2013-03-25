using ExileContactManagement.Models;
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

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(c => c
                            .Server("localhost")
                            .Database("ExileContactMgt")
                            .Username("sa")
                            .Password("eXile123")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NhibernateContext>())
              .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
              .BuildSessionFactory();
        }
    }
}