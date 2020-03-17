using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(ViewModelController viewmodels) : base(viewmodels)
        {

        }

        public void Run()
        {
            App.ShowLogin();
            App.ViewModels.Setup.Run();
        }

    }
}
