using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core;
using jotfi.Jot.Core.Views;
using jotfi.Jot.Core.Views.System;

namespace jotfi.Jot.Console.Views
{
    public class ConsoleViews : Logger, IViewFactory
    {
        public Application App { get; }
        public ISystemViews System { get; }

        public ConsoleViews(ConsoleApplication app, LogOpts opts = null) : base(opts)
        {
            App = app;
            System = new SystemViews(App, Opts);
        }        
        
    }
}
