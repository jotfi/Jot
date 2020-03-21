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
        public IStartView Start { get; protected set; }
        public IUserView User { get; protected set; }
        public ILoginView Login { get; protected set; }

        public ViewController(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
        }

        public void Init()
        {
            Items = new List<IBaseView>()
            {
                Start,
                User,
                Login
            };
        }
    }
}
