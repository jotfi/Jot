using Nista.Jottre.Base.System;
using Nista.Jottre.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.Base
{
    public abstract class BaseView : Logger, IBaseView
    {
        protected readonly ConsoleApplication Jottre;

        public BaseView(ConsoleApplication jottre, LogOpts opts = null) : base(opts)
        {
            Jottre = jottre;
        }

        protected virtual void AddToTop(Terminal.Gui.View view)
        {
            Application.Top.Add(view);
        }


        protected virtual void Quit()
        {
            if (Jottre.Quit())
            {
                Application.Top.Running = false;
            }
        }

        public Core.Application GetApp()
        {
            return Jottre;
        }

    }
}
