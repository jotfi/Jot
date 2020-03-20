using johncocom.Jot.Base.System;
using johncocom.Jot.Core.ViewModels.Base;
using johncocom.Jot.Core.Views.Base;
using johncocom.Jot.Core.Views.System;
using johncocom.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Core.ViewModels.System
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        ILoginViews GetLoginView()
        {
            return (ILoginViews)GetView();
        }

        public bool PerformLogin()
        {
            return GetLoginView().PerformLogin();
        }

        

    }
}
