using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
{
    public class SetupViewModel : BaseViewModel
    {
        public SetupViewModel(ViewModelController viewmodels) : base(viewmodels)
        {

        }

        public void Run()
        {
            var tables = App.Repository.TableNames.GetList(new { Type = "table" });
            App.Database.Setup(tables.ToList());
            if (!App.Repository.Users.Exists())
            {
                App.SetupAdmin();
            }
        }

    }
}
