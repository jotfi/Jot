using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jotfi.Jot.Core.ViewModels.System
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
            if (!CheckAdministrator(out string error))
            {
                if (!string.IsNullOrEmpty(error))
                {
                    GetApp().ShowError(error);
                }
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
            return GetRepository().System.TableName.GetList(whereConditions).ToList();
        }

        bool CheckAdministrator(out string error)
        {
            error = string.Empty;
            if (GetRepository().System.User.Exists())
            {
                return true;
            }
            var admin = new User() { UserName = "Administrator " }; 
            return GetViews().Start.SetupAdministrator(admin, out error);
        }

        public bool IsAdministratorValid(User user, out string error)
        {
            error = string.Empty;
            if (!GetViewModels().User.GetPasswordValid(user.Password.CreatePassword))
            {
                error += "Invalid password. Password must not be too weak.\r\n";
                error += GetViewModels().User.GetPasswordInfo(user.Password.CreatePassword);
                return false;
            }
            if (user.Password.CreatePassword != user.Password.ConfirmPassword)
            {
                error += "Invalid password. Confirm password does not match.";
                return false;
            }
            if (!GetViewModels().User.GetEmailValid(user.Person.Email.EmailAddress))
            {
                error += "Invalid email. Please check email address.";
                return false;
            }
            if (user.Person.Email.EmailAddress != user.Person.Email.ConfirmEmail)
            {
                error += "Invalid email. Confirm email does not match.";
                return false;
            }
            return true;
        }

        public bool SaveAdministrator(User admin, out string error)
        {
            if (!IsAdministratorValid(admin, out error))
            {
                return false;
            }
            return GetViewModels().User.CreateUser(admin);
        }

        bool CheckOrganization()
        {
            if (GetRepository().System.Organization.Exists())
            {
                return true;
            }
            return GetViews().Start.SetupOrganization();
        }

        public string CreateAdministratorText()
        {
            return $@"
Setting up {Constants.DefaultApplicationName} for the first time.
To get started, an Administrator account with full access will be created.
This account should only be used for system administration.";
        }
    }
}
