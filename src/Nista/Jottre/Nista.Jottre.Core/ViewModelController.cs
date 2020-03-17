using Nista.Jottre.Core.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core
{
    public class ViewModelController
    {
        public readonly Application App;
        public readonly LoginViewModel Login;
        public ViewModelController(Application app)
        {
            App = app;
            Login = new LoginViewModel(this);
        }
    }
}
