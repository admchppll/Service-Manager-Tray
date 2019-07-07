using ServiceManagement.Core.Interfaces;
using ServiceManager.TrayIcon.Builders;
using System.ComponentModel;
using Container = System.ComponentModel.Container;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace ServiceManager.TrayIcon
{
    public class ServiceManagerTrayIcon
    {
        public const string IconText = "Service Manager";

        protected ServiceManagerTrayIcon()
        {
        }

        public static NotifyIcon Create(IToggleWindow window)
        {
            IContainer componentModelContainer = new Container();
            var trayIconBuilder = new TrayIconBuilder(ref componentModelContainer);
            var contextMenuBuilder = new ContextMenuStripBuilder(ref componentModelContainer);

            return trayIconBuilder
                .WithIconText(IconText)
                .WithDoubleClickWindowAction(window.GetWindowToggleAction())
                .WithContextMenuStrip(contextMenuBuilder.Build())
                .Build();
        }
    }
}