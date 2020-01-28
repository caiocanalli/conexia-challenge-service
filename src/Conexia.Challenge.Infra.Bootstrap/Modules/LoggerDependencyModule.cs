using Conexia.Challenge.Infra.IoC;
using Conexia.Challenge.Infra.Logging.Interfaces;
using Conexia.Challenge.Infra.Logging.Services;
using SimpleInjector;

namespace Conexia.Challenge.Infra.Bootstrap.Modules
{
    public class LoggerDependencyModule : DependencyModule
    {
        public override void RegisterDependencies()
        {
            IoC.IoC.RecoverContainer().Register(
                typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>), Lifestyle.Scoped);
        }
    }
}
