using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.System;
using Nista.Jottre.Core.Views;

namespace Nista.Jottre.Console.Views
{
    public class ConsoleViewController : ViewController
    {
        public ConsoleViewController(ConsoleApplication jottre, LogOpts opts = null) : base(jottre, opts)
        {
            Start = new StartViews(jottre, opts);
            Login = new LoginViews(jottre, opts);
            Init();
        }
    }
}
