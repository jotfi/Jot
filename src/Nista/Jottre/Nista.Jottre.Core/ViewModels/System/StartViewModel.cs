using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Core.Views.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public void Run()
        {
            GetDatabase().Setup(GetTableNames());
            if (!GetRepository().System.Users.Exists())
            {
                //Application.Views.Setup.SetupAdmin();
            }
            GetViewModels().Login.ShowLogin();
            GetViews().Start.ApplicationStart();
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return GetRepository().System.TableNames.GetList(whereConditions).ToList();
        }
    }
}
