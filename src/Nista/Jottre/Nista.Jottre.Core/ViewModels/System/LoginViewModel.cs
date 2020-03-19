using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Core.Views.Base;
using Nista.Jottre.Core.Views.System;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
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
