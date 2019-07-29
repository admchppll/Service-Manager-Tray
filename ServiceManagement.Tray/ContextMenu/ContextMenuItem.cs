using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceManagement.Tray.ContextMenu
{
    internal abstract class ContextMenuItem
    {
        protected abstract string _label { get; }
        protected virtual Image _image { get => default(Image); }
        protected abstract Action<object, EventArgs> _action { get; }

        public virtual ToolStripItem Create()
        {
            var toolStripItem = new ToolStripMenuItem(_label, _image, new EventHandler(_action));
            toolStripItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            return toolStripItem;
        }
    }
}