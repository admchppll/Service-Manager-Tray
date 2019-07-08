using System.Collections.Generic;

namespace ServiceManager.TrayIcon.ContextMenu
{
    public abstract class ContextMenuItemFactory
    {
        public abstract Dictionary<int, ContextMenuItem> CreateMenuItems();
    }
}