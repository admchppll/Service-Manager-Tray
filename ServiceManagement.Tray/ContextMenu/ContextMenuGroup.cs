using System.Collections.Generic;

namespace ServiceManagement.Tray.ContextMenu
{
    internal class ContextMenuGroup
    {
        internal int Priority { get; set; }
        internal List<ContextMenuItem> MenuItems { get; set; }

        internal ContextMenuGroup()
        { }
    }

    internal class ContextMenuGroupComparer : IComparer<ContextMenuGroup>
    {
        public int Compare(ContextMenuGroup x, ContextMenuGroup y)
        {
            if (x.Priority > y.Priority)
                return 1;

            if (x.Priority < y.Priority)
                return -1;

            return 0;
        }
    }
}