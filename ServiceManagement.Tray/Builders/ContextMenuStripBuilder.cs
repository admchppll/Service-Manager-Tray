using ServiceManagement.Core.Interfaces;
using ServiceManagement.Tray.ContextMenu.Items;
using System;
using Container = System.ComponentModel.IContainer;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;

namespace ServiceManagement.Tray.Builders
{
    public class ContextMenuStripBuilder : IBuilder<ContextMenuStrip>
    {
        private readonly Action<object, EventArgs> _exitFunction = delegate (object sender, EventArgs args)
        {
            //System.Windows.Application.Current.Shutdown();
        };

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
            contextMenuStrip.Items.Add("Add", null, new EventHandler(_exitFunction));
            contextMenuStrip.Items.Add(SettingsItem.Create());
            contextMenuStrip.Items.Add(CloseProgramItem.Create());
        }
    }
}