using johncocom.Jot.Core.ViewModels.System;
using johncocom.Jot.Core.Views.Base;

namespace johncocom.Jot.Core.Views.System
{
    public interface IStartViews : IBaseView
    {
        StartViewModel GetStartViewModel();
        void ApplicationStart();
        bool SetupAdministrator();
        bool SetupOrganization();
    }
}
