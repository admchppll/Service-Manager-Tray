using ServiceManagement.Core.Startup;
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

            var trayContainer = new System.ComponentModel.Container();
            trayIcon = new System.Windows.Forms.NotifyIcon(trayContainer)
            {
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(),
                Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                Visible = true,
                Icon = new System.Drawing.Icon("icon.ico")
            };

            trayIcon.DoubleClick += delegate (object sender, EventArgs args)
            {
                if (!window.IsVisible)
                    window.Show();
                else
                    window.Hide();

                window.WindowState = WindowState.Normal;
            };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}