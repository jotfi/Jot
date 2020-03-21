using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core.Views;

namespace jotfi.Jot.Console.Views
{
    public class ConsoleViewController : ViewController
    {
        public ConsoleViewController(ConsoleApplication app, LogOpts opts = null) : base(app, opts)
        {
            Start = new StartView(app, app.ViewModels.Start, opts);
            User = new UserView(app, app.ViewModels.User, opts);
            Login = new LoginView(app, app.ViewModels.Login, opts);
            Init();
        }
    }
}
