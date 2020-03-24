using jotfi.Jot.Base.System;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.ViewModels
{
    public class ViewModelFactory : Logger
    {
        public readonly Application App;
        public readonly SystemViewModel System;
        public readonly UserViewModel User;
        public readonly LoginViewModel Login;

        public ViewModelFactory(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            System = new SystemViewModel(app, opts);
            User = new UserViewModel(app, opts);
            Login = new LoginViewModel(app, opts);
        }

    }
}
