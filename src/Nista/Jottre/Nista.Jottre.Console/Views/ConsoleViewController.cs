using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.System;
using Nista.Jottre.Core.Views;

namespace Nista.Jottre.Console.Views
{
    public class ConsoleViewController : ViewController
    {
        public ConsoleViewController(ConsoleApplication app, LogOpts opts = null) : base(app, opts)
        {
            Login = new LoginViews(app, opts);
            Setup = new SetupViews(app, opts);
            Init();
        }
    }
}
