using ServiceManagement.Core.Clients;
using ServiceManagement.Core.Services;
using Unity;
using Unity.Lifetime;

namespace ServiceManagement.Core.Startup
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

            container.RegisterType<IServiceClient, ServiceClient>(new TransientLifetimeManager());

            container.RegisterType<IDescriptionService, DescriptionService>(new TransientLifetimeManager());
            container.RegisterType<IStatusService, StatusService>(new TransientLifetimeManager());
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