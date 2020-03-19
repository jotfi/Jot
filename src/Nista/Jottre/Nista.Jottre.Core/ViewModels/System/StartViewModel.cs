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
            if (!GetDatabase().CheckTables(GetTableNames()))
            {
                return;
            }
            if (!CheckAdministrator())
            {
                return;
            }
            if (!CheckOrganization())
            {
                return;
            }
            while (!GetApp().IsLoggedIn())
            {
                if (!GetViewModels().Login.PerformLogin())
                {
                    break;
                }
            }            
            if (!GetApp().IsLoggedIn())
            {
                return;
            }
            GetViews().Start.ApplicationStart();
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return GetRepository().System.TableNames.GetList(whereConditions).ToList();
        }

        bool CheckAdministrator()
        {
            if (GetRepository().System.Users.Exists())
            {
                return true;
            }
            return GetViews().Start.SetupAdministrator();
        }

        public bool SaveAdministrator()
        {
            return false;
        }

        bool CheckOrganization()
        {
            if (GetRepository().System.Organizations.Exists())
            {
                return true;
            }
            return GetViews().Start.SetupOrganization();
        }

        public string CreateAdministratorText()
        {
            return @"
Setting up Jottre for the first time.
To get started, an Administrator account with full access will be created.
This account should only be used for system administration.";
        }
    }
}
