using Conexia.Challenge.Infra.Bootstrap.Resolver;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Conexia.Challenge.Services.Api.Extensions
{
    public static class SimpleInjectorExtension
    {
        static Container _container = Infra.IoC.IoC.RecoverContainer();

        public static void AddSimpleInjectorConfig(this IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle =
                new AsyncScopedLifestyle();

            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();

                options
                    .AddLogging();
            });

            RegisterDependencies();
        }

        public static void UseSimpleInjectorConfig(this IApplicationBuilder app)
        {
            app.UseSimpleInjector(_container);
        }

        static void RegisterDependencies() =>
            DependencyResolver.RegisterDependencies();
    }
}
