using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;

namespace jotfi.Jot.Core.Views.System
{
    public interface IStartViews : IBaseView
    {
        StartViewModel GetStartViewModel();
        void ApplicationStart();
        bool SetupAdministrator();
        bool SetupOrganization();
    }
}
