using Nista.Jottre.Base.Log;
using Nista.Jottre.Core;
using Nista.Jottre.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console.Views.Base
{
    public abstract class BaseView : Logging, IBaseView
    {
        protected readonly ConsoleApplication App;
        protected readonly Terminal.Gui.Window Window;
        
        public BaseView(ConsoleViewController win, string title,
            bool isConsole = true, Action<string> showLog = null) : base(isConsole, showLog)
        {
            App = win.App;
            Window = new Terminal.Gui.Window(title)
            {
                X = 0,
                Y = 1,
                Width = Terminal.Gui.Dim.Fill(),
                Height = Terminal.Gui.Dim.Fill()
            };
        }

        public Application GetApp()
        {
            return App;
        }

    }
}
