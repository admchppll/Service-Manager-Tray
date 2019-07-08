using System;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace ServiceManager.TrayIcon.ContextMenu.Items
{
    public class CloseProgramItem
    {
        private const string _label = "Close";
        private static readonly Image _image = default(Image);

        private static readonly Action<object, EventArgs> _action = delegate (object sender, EventArgs args)
        {
            System.Windows.Application.Current.Shutdown();
        };

        protected CloseProgramItem()
        {
        }

        public static ToolStripItem Create()
        {
            var x = new ToolStripMenuItem(_label, _image, new EventHandler(_action));
            x.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            return x;
        }
    }
}