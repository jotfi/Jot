using Nista.Jottre.Core.Views.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core
{
    public abstract class ViewController
    {
        public List<IBaseView> Items { get; private set; }
        public ILoginViews Login { get; protected set; }
        public ISetupViews Setup { get; protected set; }

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
