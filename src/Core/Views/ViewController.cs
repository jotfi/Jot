using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Views
{
    public abstract class ViewController : Logger
    {
        public readonly Application App;
        public List<IBaseView> Items { get; private set; }
        public IStartViews Start { get; protected set; }
        public ILoginViews Login { get; protected set; }

        public ViewController(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
        }

        public void Init()
        {
            Items = new List<IBaseView>()
            {
                Start,
                Login
            };
        }
    }
}
