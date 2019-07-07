using System;

namespace ServiceManagement.Core.Interfaces
{
    public interface IToggleWindow
    {
        Action<object, EventArgs> GetWindowToggleAction();
    }
}