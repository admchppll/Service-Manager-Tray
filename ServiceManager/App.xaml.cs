using ServiceManagement.Core.Startup;
using ServiceManager.TrayIcon;
using System.Windows;
using Unity;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon _trayIcon;
        private IUnityContainer _container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UnityBootstrapper.Register(_container);

            var window = _container.Resolve<MainWindow>();

            _trayIcon = ServiceManagerTrayIcon.Create(window);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}