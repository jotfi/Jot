using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.System
{
    public class StartViews : BaseView, IStartViews
    {
        public StartViews(ConsoleApplication jottre, LogOpts opts = null) : base(jottre, opts)
        {

        }

        public void ApplicationStart()
        {
            Application.Run();
        }

        public void SetupAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
