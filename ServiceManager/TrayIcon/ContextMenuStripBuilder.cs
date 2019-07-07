using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using Container = System.ComponentModel.Container;
using System;

namespace ServiceManager.TrayIcon
{
    public class ContextMenuStripBuilder
    {
        private readonly Container _container;

        public ContextMenuStripBuilder(ref Container container)
        {
            _container = container;
        }

        public ContextMenuStrip Build()
        {
            var menuStrip = new ContextMenuStrip(_container);
            AddExitOption(ref menuStrip);
            return menuStrip;
        }

        public void AddExitOption(ref ContextMenuStrip contextMenuStrip)
        {
            Action<object, EventArgs> exit = delegate (object sender, EventArgs args)
            {
                System.Windows.Application.Current.Shutdown();
            };

            var exitHandler = new EventHandler(exit);

            contextMenuStrip.Items.Add("Exit", null, exitHandler);
        }
    }
}