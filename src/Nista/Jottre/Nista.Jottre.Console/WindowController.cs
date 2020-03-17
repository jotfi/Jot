using Nista.Jottre.Console.Windows.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console
{
    public class WindowController
    {
        public readonly GuiApplication App;
        public readonly LoginWindow Login;

        public WindowController(GuiApplication app)
        {
            App = app;
            Login = new LoginWindow(this);
        }
    }
}
