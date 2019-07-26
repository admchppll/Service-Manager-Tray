using System;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using IContainer = System.ComponentModel.IContainer;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace ServiceManagement.Tray.Builders
{
    internal class TrayIconBuilder
    {
        private const string DefaultIconPath = "icon.ico";

        private ContextMenuStrip _contextMenuStrip;
        private string _trayIconText = string.Empty;
        private Action<object, EventArgs> _doubleClickAction;
        private System.Drawing.Icon _icon;

        private readonly IContainer _container;

        public TrayIconBuilder(ref IContainer container)
        {
            _container = container;
        }

        public TrayIconBuilder WithIconText(string iconText)
        {
            _trayIconText = iconText;
            return this;
        }

        public TrayIconBuilder WithIcon(string iconPath)
        {
            _icon = new System.Drawing.Icon(iconPath);
            return this;
        }

        public TrayIconBuilder WithDoubleClickWindowAction(Action<object, EventArgs> action)
        {
            _doubleClickAction = action;
            return this;
        }

        public TrayIconBuilder WithContextMenuStrip(ContextMenuStrip contextMenuStrip)
        {
            _contextMenuStrip = contextMenuStrip;
            return this;
        }

        public NotifyIcon Build()
        {
            var trayIcon = CreateBaseNotifyIcon();
            AddContextMenuStrip(ref trayIcon);
            AddDoubleClickDelegate(ref trayIcon);
            return trayIcon;
        }

        private NotifyIcon CreateBaseNotifyIcon()
        {
            return new NotifyIcon(_container)
            {
                Text = GetIconText(),
                Icon = GetIcon(),
                Visible = true
            };
        }

        private void AddDoubleClickDelegate(ref NotifyIcon trayIcon)
        {
            if (_doubleClickAction != null)
            {
                trayIcon.DoubleClick += delegate (object sender, EventArgs args)
                {
                    _doubleClickAction(sender, args);
                };
            }
        }

        private void AddContextMenuStrip(ref NotifyIcon trayIcon)
        {
            if (_contextMenuStrip != null)
                trayIcon.ContextMenuStrip = _contextMenuStrip;
        }

        private System.Drawing.Icon GetIcon()
        {
            return _icon != null
                ? _icon
                : new System.Drawing.Icon(DefaultIconPath);
        }

        private string GetIconText()
        {
            return string.IsNullOrWhiteSpace(_trayIconText)
                ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
                : _trayIconText;
        }
    }
}