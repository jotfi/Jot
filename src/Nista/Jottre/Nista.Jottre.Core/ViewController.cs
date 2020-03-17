using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core
{
    public abstract class ViewController
    {
        public ILoginView Login { get; protected set; }
        public ISetupView Setup { get; protected set; }
    }
}
