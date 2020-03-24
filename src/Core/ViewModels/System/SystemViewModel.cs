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
    public class SystemViewModel : BaseViewModel
    {

        public SystemViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public void CheckDatabase() => GetDatabase().CheckTables(GetTableNames());
        public bool CheckAdministrator() => GetRepository().System.User.Exists();
        public bool CheckOrganization() => GetRepository().System.Organization.Exists();

        public User CreateAdminUser()
        {
            var admin = new User() { UserName = "Administrator" };
            admin.Person.FirstName = "Admin";
            admin.Person.LastName = "System";
#if DEBUG
            admin.Password.CreatePassword = "admin1!";
            admin.Password.ConfirmPassword = admin.Password.CreatePassword;
            admin.Person.Email.EmailAddress = "admin@admin.com";
            admin.Person.Email.ConfirmEmail = admin.Person.Email.EmailAddress;
#endif
            return admin;
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return GetRepository().System.TableName.GetList(whereConditions).ToList();
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

        public string CreateAdministratorText()
        {
            return $@"
Setting up {Constants.DefaultApplicationName} for the first time.
To get started, an Administrator account with full access will be created.
This account should only be used for system administration.";
        }
    }
}
