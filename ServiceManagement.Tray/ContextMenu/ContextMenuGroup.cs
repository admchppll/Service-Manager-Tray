using System.Collections.Generic;

namespace ServiceManagement.Tray.ContextMenu
{
    public class ContextMenuGroup
    {
        public int Priority { get; set; }
        public List<ContextMenuItem> MenuItems { get; set; }

        public ContextMenuGroup()
        { }
    }

    public class ContextMenuGroupComparer : IComparer<ContextMenuGroup>
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