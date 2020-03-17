using Nista.Jottre.Console.Windows.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Windows.System
{
    public class SetupWindow : BaseWindow, ISetupView
    {
        public SetupWindow(WindowController win) : base(win, "Jottre - Setup")
        {

        }

        public void SetupAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
