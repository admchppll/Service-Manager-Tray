using System;

namespace ServiceManager.TrayIcon
{
    public class TrayIconBuilder
    {
        private const string DefaultIconPath = "icon.ico";

        private System.Windows.Forms.NotifyIcon _trayIcon;
        private string _trayIconText = string.Empty;
        private Action<object, EventArgs> _doubleClickAction;
        private System.Drawing.Icon _icon;

        public TrayIconBuilder()
        {
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

        public TrayIconBuilder WithDoubleClickWindowAction(Action<object, EventArgs> eventHandler)
        {
            _doubleClickAction = eventHandler;
            return this;
        }

        public System.Windows.Forms.NotifyIcon Build()
        {
            CreateBaseNotifyIcon();
            AddDoubleClickDelegate();
            return _trayIcon;
        }

        private void CreateBaseNotifyIcon()
        {
            var trayContainer = new System.ComponentModel.Container();
            _trayIcon = new System.Windows.Forms.NotifyIcon(trayContainer)
            {
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(),
                Text = GetIconText(),
                Visible = true,
                Icon = GetIcon()
            };
        }

        private void AddDoubleClickDelegate()
        {
            if (_doubleClickAction != null)
                _trayIcon.DoubleClick += delegate (object sender, EventArgs args)
                {
                    _doubleClickAction(sender, args);
                };
        }

        private System.Drawing.Icon GetIcon()
        {
            if (_icon != null)
            {
                return _icon;
            }

            return new System.Drawing.Icon(DefaultIconPath);
        }

        private string GetIconText()
        {
            return string.IsNullOrWhiteSpace(_trayIconText)
                ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
                : _trayIconText;
        }
    }
}