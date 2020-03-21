using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Core.Views.System
{
    public interface IStartView : IBaseView
    {
        StartViewModel GetStartViewModel();
        void ApplicationStart();
        bool SetupAdministrator(User admin, out string error);
        bool SetupOrganization();
    }
}
