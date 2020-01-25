using Conexia.Challenge.Infra.Data.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Conexia.Challenge.Infra.Data.Providers
{
    public class SessionProvider
    {
        readonly string _connectionString;
        ISessionFactory _sessionFactory;

        public SessionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(_connectionString)
                    .ShowSql()
                    .FormatSql()
                    .Driver<NHibernate.Driver.SqlClientDriver>)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DocumentRepository>())
                .BuildSessionFactory();
        }
    }
}
