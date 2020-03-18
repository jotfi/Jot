using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.System
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel(Application jottre, LogOpts opts = null) : base(jottre, opts)
        {

        }

        public void Run()
        {
            Jottre.Database.Setup(GetTableNames());
            if (!Jottre.Repository.System.Users.Exists())
            {
                //Application.Views.Setup.SetupAdmin();
            }
            Jottre.ViewModels.Login.Run();
            Jottre.Views.Start.ApplicationStart();
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return Jottre.Repository.System.TableNames.GetList(whereConditions).ToList();
        }
    }
}
