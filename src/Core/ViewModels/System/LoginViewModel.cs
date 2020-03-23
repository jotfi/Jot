using jotfi.Jot.Base.System;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Core.Views.System;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        ILoginView GetLoginView()
        {
            return (ILoginView)GetView();
        }

        public bool PerformLogin()
        {
            return GetLoginView().PerformLogin();
        }

        

    }
}
