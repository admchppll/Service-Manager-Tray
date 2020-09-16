using ServiceManager.Startup;
using ServiceManagement.Data.Database;
using ServiceManagement.Data.Repositories;
using ServiceManagement.Data.Repositories.Interfaces;
using ServiceManagement.Services;
using ServiceManagement.Services.Interfaces;
using ServiceManagement.Tray;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon _trayIcon;
        private IUnityContainer _container = new UnityContainer();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await DbInitialise.Initialise();

            _container.RegisterType<IServiceRepository, ServiceRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWatchedServiceRepository, WatchedServiceRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDescriptionService, DescriptionService>(new TransientLifetimeManager());
            _container.RegisterType<IStatusService, StatusService>(new TransientLifetimeManager());
            UnityBootstrapper.Register(_container);

            var window = _container.Resolve<MainWindow>();
            _trayIcon = Icon.Create(window);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}