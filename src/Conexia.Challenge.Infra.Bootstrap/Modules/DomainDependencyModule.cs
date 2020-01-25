using Conexia.Challenge.Domain.Documents;
using Conexia.Challenge.Domain.Documents.Interfaces;
using Conexia.Challenge.Infra.IoC;
using SimpleInjector;

namespace Conexia.Challenge.Infra.Bootstrap.Modules
{
    public class DomainDependencyModule : DependencyModule
    {
        public override void RegisterDependencies()
        {
            Container container = IoC.IoC.RecoverContainer();

            container.Register<
                IDocumentService,
                DocumentService>(Lifestyle.Scoped);
        }
    }
}
