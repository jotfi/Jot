using Nista.Jottre.Base.System;
using Nista.Jottre.Core.Views.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.Views
{
    public abstract class ViewController : Logger
    {
        public readonly Application Application;
        public List<IBaseView> Items { get; private set; }
        public ILoginViews Login { get; protected set; }
        public ISetupViews Setup { get; protected set; }

        public ViewController(Application app, LogOpts opts = null) : base(opts)
        {
            Application = app;
        }

        public void Init()
        {
            Items = new List<IBaseView>()
            {
                Login,
                Setup
            };
        }
    }
}
