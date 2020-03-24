using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Core.Views.System
{
    public interface ISystemView : IBaseView
    {
        SystemViewModel GetSystemViewModel();
        void ApplicationStart();
        bool SetupAdministrator(User admin, out string error);
        bool SetupOrganization(Organization organization, out string error);
    }
}
