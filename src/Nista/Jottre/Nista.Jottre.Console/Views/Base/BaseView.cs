using Nista.Jottre.Base.System;
using Nista.Jottre.Core;
using Nista.Jottre.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console.Views.Base
{
    public abstract class BaseView : Logger, IBaseView
    {
        protected readonly ConsoleApplication Application;
        
        public BaseView(ConsoleApplication app, LogOpts opts = null) : base(opts)
        {
            Application = app;
        }

        protected virtual void AddToTop(Terminal.Gui.View view)
        {
            Application.Top.Add(view);
        }

        protected virtual Terminal.Gui.Window GetWindow(string title = "")
        {
            return new Terminal.Gui.Window(title)
            {
                X = 0,
                Y = 1,
                Width = Terminal.Gui.Dim.Fill(),
                Height = Terminal.Gui.Dim.Fill()
            };
        }

        protected virtual void Quit()
        {
            if (Application.Quit())
            {
                Application.Top.Running = false;
            }
        }

        public Application GetApp()
        {
            return Application;
        }

    }
}
