using Nista.Jottre.Console.Views.System;
using Nista.Jottre.Core.Views;

namespace Nista.Jottre.Console.Views
{
    public class ConsoleViewController : ViewController
    {
        public readonly ConsoleApplication App;

        public ConsoleViewController(ConsoleApplication app)
        {
            App = app;
            Login = new LoginViews(app);
            Setup = new SetupViews(app);
            Init();
        }
    }
}
