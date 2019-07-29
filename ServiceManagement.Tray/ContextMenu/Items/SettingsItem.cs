using System;

//using Application = System.Windows.Application;

namespace ServiceManagement.Tray.ContextMenu.Items
{
    internal class SettingsItem : ContextMenuItem
    {
        protected override string _label => "Settings";

        protected override Action<object, EventArgs> _action => delegate (object sender, EventArgs args)
        {
            //            if (!Application.Current.Windows.OfType<ServiceSettingWindow>().Any())
            //            {
            //                UnityBootstrapper.Resolve<ServiceSettingWindow>().Show();
            //            }
        };
    }
}