﻿using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(Application jottre, LogOpts opts = null) : base(jottre, opts)
        {

        }

        public void Run()
        {
            Jottre.Views.Login.ShowLogin();
        }

    }
}
