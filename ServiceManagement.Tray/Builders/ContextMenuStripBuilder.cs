using ServiceManagement.Core.Interfaces;
using ServiceManagement.Tray.ContextMenu;
using ServiceManagement.Tray.ContextMenu.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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

        private readonly List<ContextMenuGroup> _menuItems = new List<ContextMenuGroup>()
        {
            new ContextMenuGroup(){
                Priority = 99,
                MenuItems = new List<ContextMenuItem>() {
                    new SettingsItem(),
                    new CloseProgramItem()
                }
            },
            new ContextMenuGroup(){
                Priority = 2,
                MenuItems = new List<ContextMenuItem>() {
                    new SettingsItem(),
                    new CloseProgramItem()
                }
            }
        };

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
            var comparer = new ContextMenuGroupComparer();
            _menuItems.Sort(comparer);

            var lastItem = _menuItems.Last();
            foreach (var itemGroup in _menuItems)
            {
                foreach (var item in itemGroup.MenuItems)
                    contextMenuStrip.Items.Add(item.Create());

                if (!itemGroup.Equals(lastItem))
                    contextMenuStrip.Items.Add(new ToolStripSeparator());
            }
        }
    }
}