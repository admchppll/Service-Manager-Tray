using ServiceManagement.Core.Proxies;
using ServiceManagement.Core.Repositories;
using ServiceManagement.Core.Services;
using System;
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

            container.RegisterType<IServiceProxy, ServiceProxy>(new TransientLifetimeManager());
            container.RegisterType<IServiceRepository, ServiceRepository>(new TransientLifetimeManager());
            container.RegisterType<IWatchedServiceRepository, WatchedServiceRepository>(new TransientLifetimeManager());
            container.RegisterType<IDescriptionService, DescriptionService>(new TransientLifetimeManager());
            container.RegisterType<IStatusService, StatusService>(new TransientLifetimeManager());
        }

        public static object Resolve(Type type)
        {
            return Container.Resolve(type, string.Empty);
        }

        protected UnityBootstrapper()
        {
        }
    }
}