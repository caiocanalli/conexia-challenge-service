using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Conexia.Challenge.Infra.IoC
{
    public class IoC
    {
        static Container _container;

        static IoC()
        {
            InitializeContainer();
        }

        static void InitializeContainer()
        {
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;
            _container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
        }

        public static Container RecoverContainer() => _container;
    }
}
