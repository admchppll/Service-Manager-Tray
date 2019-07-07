using ServiceManagement.Core.Startup;
using ServiceManager.TrayIcon;
using System;
using System.Windows;
using Unity;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            UnityBootstrapper.Register(container);

            var window = container.Resolve<MainWindow>();

            var trayIconBuilder = new TrayIconBuilder();
            trayIcon = trayIconBuilder
                .WithIconText("Service Manager")
                .WithDoubleClickWindowAction(window.GetWindowToggleAction())
                .Build();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}