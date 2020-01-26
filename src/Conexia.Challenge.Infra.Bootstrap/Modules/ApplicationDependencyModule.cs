using Conexia.Challenge.Application.Documents.Factories.Types;
using Conexia.Challenge.Application.Documents.Factories.Types.Interfaces;
using Conexia.Challenge.Application.Documents.Services;
using Conexia.Challenge.Application.Documents.Services.Interfaces;
using Conexia.Challenge.Application.Documents.Strategies;
using Conexia.Challenge.Application.Documents.Strategies.Types;
using Conexia.Challenge.Application.Documents.Strategies.Types.Interfaces;
using Conexia.Challenge.Application.Users.Services;
using Conexia.Challenge.Application.Users.Services.Interfaces;
using Conexia.Challenge.Infra.IoC;
using SimpleInjector;

namespace Conexia.Challenge.Infra.Bootstrap.Modules
{
    public class ApplicationDependencyModule : DependencyModule
    {
        public override void RegisterDependencies()
        {
            Container container = IoC.IoC.RecoverContainer();

            // Document

            container.Register<
                IDocumentAppService,
                DocumentAppService>(Lifestyle.Scoped);

            // Factories

            container.Register<
                ITypeFactory,
                TypeFactory>(Lifestyle.Scoped);

            // Strategies

            container.Collection.Register<ITypeStrategy>(
                typeof(CsvStrategy),
                typeof(XlsStrategy));

            container.Register<CsvStrategy>(Lifestyle.Scoped);
            container.Register<XlsStrategy>(Lifestyle.Scoped);

            // User

            container.Register<
                IUserAppService,
                UserAppService>(Lifestyle.Scoped);
        }
    }
}
