using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.System;
using Nista.Jottre.Core.Views;

namespace Nista.Jottre.Console.Views
{
    public class ConsoleViewController : ViewController
    {
        public ConsoleViewController(ConsoleApplication app, LogOpts opts = null) : base(app, opts)
        {
            Start = new StartViews(app, app.ViewModels.Start, opts);
            Login = new LoginViews(app, app.ViewModels.Login, opts);
            Init();
        }
    }
}
