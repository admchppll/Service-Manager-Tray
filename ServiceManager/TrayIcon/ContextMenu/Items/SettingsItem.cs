using ServiceManagement.Core.Startup;
using System;
using System.Linq;
using System.Windows.Forms;
using Application = System.Windows.Application;
using Image = System.Drawing.Image;

namespace ServiceManager.TrayIcon.ContextMenu.Items
{
    public class SettingsItem
    {
        private const string _label = "Settings";
        private static readonly Image _image = default(Image);

        private static readonly Action<object, EventArgs> _action = delegate (object sender, EventArgs args)
        {
            if (!Application.Current.Windows.OfType<ServiceSettingWindow>().Any())
            {
                UnityBootstrapper.Resolve<ServiceSettingWindow>().Show();
            }
        };

        protected SettingsItem()
        {
        }

        public static ToolStripItem Create()
        {
            var toolStripItem = new ToolStripMenuItem(_label, _image, new EventHandler(_action));
            toolStripItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            return toolStripItem;
        }
    }
}