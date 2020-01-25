using AutoMapper;
using Conexia.Challenge.Application.Documents.Mappers;
using Conexia.Challenge.Infra.IoC;
using SimpleInjector;

namespace Conexia.Challenge.Infra.Bootstrap.Modules
{
    public class AutoMapperDependencyModule : DependencyModule
    {
        public override void RegisterDependencies()
        {
            Container container = IoC.IoC.RecoverContainer();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DocumentProfile());
            });

            container.RegisterInstance<IConfigurationProvider>(config);

            container.Register<IMapper>(() => new Mapper(
                container.GetInstance<IConfigurationProvider>(),
                type => container.GetInstance(type)), Lifestyle.Singleton);
        }
    }
}
