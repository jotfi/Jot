using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels
{
    public class ViewModelController : Logger
    {
        public readonly Application Jottre;
        public readonly LoginViewModel Login;
        public readonly StartViewModel Start;

        public ViewModelController(Application jottre, LogOpts opts = null) : base(opts)
        {
            Jottre = jottre;
            Login = new LoginViewModel(jottre);
            Start = new StartViewModel(jottre);
        }
    }
}
