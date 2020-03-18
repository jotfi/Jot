using Nista.Jottre.Console.Views.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.System
{
    public class SetupViews : BaseView, ISetupViews
    {
        public SetupViews(ConsoleApplication app) : base(app)
        {

        }

        public void SetupAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
