using Nista.Jottre.Base.System;
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
        public SetupViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public void Run()
        {
            Application.Database.Setup(GetTableNames());
            if (!Application.Repository.System.Users.Exists())
            {
                //Application.Views.Setup.SetupAdmin();
            }
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return Application.Repository.System.TableNames.GetList(whereConditions).ToList();
        }
    }
}
