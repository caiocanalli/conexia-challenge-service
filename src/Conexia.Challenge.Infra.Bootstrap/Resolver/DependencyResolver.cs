using Conexia.Challenge.Infra.Bootstrap.Modules;

namespace Conexia.Challenge.Infra.Bootstrap.Resolver
{
    public class DependencyResolver
    {
        public static void RegisterDependencies()
        {
            new ApplicationDependencyModule().RegisterDependencies();
            new AutoMapperDependencyModule().RegisterDependencies();
            new DomainDependencyModule().RegisterDependencies();
            new DataDependencyModule().RegisterDependencies();
            new LoggerDependencyModule().RegisterDependencies();
        }
    }
}
