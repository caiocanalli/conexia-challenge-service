using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Documents.Interfaces;
using Conexia.Challenge.Domain.Users.Interfaces;
using Conexia.Challenge.Infra.Data;
using Conexia.Challenge.Infra.Data.Factories;
using Conexia.Challenge.Infra.Data.Providers;
using Conexia.Challenge.Infra.Data.Repositories;
using Conexia.Challenge.Infra.IoC;
using NHibernate;
using SimpleInjector;

namespace Conexia.Challenge.Infra.Bootstrap.Modules
{
    public class DataDependencyModule : DependencyModule
    {
        public override void RegisterDependencies()
        {
            Container container = IoC.IoC.RecoverContainer();

            var sessionProvider = new SessionProvider(
                "Server=localhost,5434;Database=GRN;User Id=sa;Password=N3som40@");

            container.Register(() =>
                sessionProvider.SessionFactory, Lifestyle.Singleton);

            container.Register(() =>
                container.GetInstance<ISessionFactory>().OpenSession(), Lifestyle.Scoped);

            container.Register<
                IUnitOfWorkFactory,
                UnitOfWorkFactory>(Lifestyle.Scoped);

            container.Register<
                IUnitOfWork,
                UnitOfWork>(Lifestyle.Scoped);

            container.Register<
                IDocumentRepository,
                DocumentRepository>(Lifestyle.Scoped);

            container.Register<
                IUserRepository,
                UserRepository>(Lifestyle.Scoped);
        }
    }
}
