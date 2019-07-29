using System;

namespace ServiceManagement.Tray.ContextMenu.Items
{
    internal class CloseProgramItem : ContextMenuItem
    {
        protected override string _label { get => "Close"; }

        protected override Action<object, EventArgs> _action => delegate (object sender, EventArgs args)
        {
            //System.Windows.Application.Current.Shutdown();
        };
    }
}