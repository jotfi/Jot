using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core.Views;

namespace jotfi.Jot.Console.Views
{
    public class ConsoleViewFactory : ViewFactory
    {
        public ConsoleViewFactory(ConsoleApplication app, LogOpts opts = null) : base(app, opts)
        {
            System = new SystemView(app, app.ViewModels.System, opts);
            User = new UserView(app, app.ViewModels.User, opts);
            Login = new LoginView(app, app.ViewModels.Login, opts);
        }
    }
}
