using Nista.Jottre.Core.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels
{
    public class ViewModelController
    {
        public readonly Application App;
        public readonly LoginViewModel Login;
        public readonly SetupViewModel Setup;

        public ViewModelController(Application app)
        {
            App = app;
            Login = new LoginViewModel(this);
            Setup = new SetupViewModel(this);
        }
    }
}
