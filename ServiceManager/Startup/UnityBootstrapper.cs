using ServiceManagement.Core.Clients;
using ServiceManagement.Services;
using ServiceManagement.Services.Interfaces;
using Unity;
using Unity.Lifetime;

namespace ServiceManager.Startup
{
    public class UnityBootstrapper
    {
        public static IUnityContainer Container
        {
            get; private set;
        }

        public static void Register(IUnityContainer container)
        {
            Container = container;

            container.RegisterType<IServiceClient, StatusService>(new TransientLifetimeManager());
        }

        public static t Resolve<t>()
        {
            return (t)Container.Resolve(typeof(t), string.Empty);
        }

        protected UnityBootstrapper()
        {
        }
    }
}