using johncocom.Jot.Base.System;
using johncocom.Jot.Console.Views.System;
using johncocom.Jot.Core.Views;

namespace johncocom.Jot.Console.Views
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
