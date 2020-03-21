using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core.Views;

namespace jotfi.Jot.Console.Views
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
