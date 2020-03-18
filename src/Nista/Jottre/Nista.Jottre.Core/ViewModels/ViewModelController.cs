using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels
{
    public class ViewModelController : Logger
    {
        public readonly Application App;
        public readonly LoginViewModel Login;
        public readonly SetupViewModel Setup;

        public ViewModelController(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Login = new LoginViewModel(app);
            Setup = new SetupViewModel(app);
        }
    }
}
