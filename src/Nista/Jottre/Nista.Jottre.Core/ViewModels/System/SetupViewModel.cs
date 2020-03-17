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
            App.Database.Setup(GetTableNames());
            if (!App.Repository.Users.Exists())
            {
                //App.Views.Setup.SetupAdmin();
            }
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return App.Repository.TableNames.GetList(whereConditions).ToList();
        }
    }
}
