using Nista.Jottre.Console.Views.System;
using Nista.Jottre.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console
{
    public class WindowController : ViewController
    {
        public readonly GuiApplication App;

        public WindowController(GuiApplication app)
        {
            App = app;
            Login = new LoginViews(this);
            Setup = new SetupViews(this);
        }
    }
}
