using System;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace ServiceManager.TrayIcon.ContextMenu
{
    public class ContextMenuItem : ToolStripItem
    {
        protected ContextMenuItem()
        { }

        public ContextMenuItem(string label, Image image, Action<object, EventArgs> action)
            : base(label, image, new EventHandler(action))
        {
        }
    }
}