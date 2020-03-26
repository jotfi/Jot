﻿using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Core.Views.System
{
    public interface ISetupView
    {
        void ApplicationStart();
        void ApplicationEnd();
        bool SetupConnection();
        bool SetupAdministrator(User admin);
        bool SetupOrganization(Organization organization);
    }
}