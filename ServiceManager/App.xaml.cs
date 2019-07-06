using ServiceManagement.Core.Startup;
using System.Windows;
using Unity;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            UnityBootstrapper.Register(container);

            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }
}