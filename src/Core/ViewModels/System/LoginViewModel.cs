using jotfi.Jot.Base.System;
using jotfi.Jot.Core.ViewModels.Base;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public bool PerformLogin()
        {
            return false;
        }

        

    }
}
