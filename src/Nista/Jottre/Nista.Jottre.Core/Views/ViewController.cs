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
        public readonly Application Jottre;
        public List<IBaseView> Items { get; private set; }
        public IStartViews Start { get; protected set; }
        public ILoginViews Login { get; protected set; }

        public ViewController(Application jottre, LogOpts opts = null) : base(opts)
        {
            Jottre = jottre;
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
