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

        public bool CheckConnection() => false;

        public bool CheckDatabase(out string error) => GetDatabase().CheckTables(GetTableNames(), out error);
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

        public string CreateAdministratorText()
        {
            return $@"
Setting up {Constants.DefaultApplicationName} for the first time.
To get started, an Administrator account with full access will be created.
This account should only be used for system administration.";
        }

        public string FirstOrganizationText()
        {
            return $@"
Logging into {Constants.DefaultApplicationName} requires an organization.
Please enter an organizaton name, this can be edited later.";
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

        public bool IsOrganizationValid(Organization organization, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(organization.Name))
            {
                error += "Invalid organization. Name must not be blank.";
                return false;
            }
            return true;
        }

        public bool SaveOrganization(Organization organization, out string error)
        {
            if (!IsOrganizationValid(organization, out error))
            {
                return false;
            }
            return CreateOrganization(organization);
        }

        public bool CreateOrganization(Organization organization)
        { 
            try
            {
                using var uow = GetDatabase().Context.Create();
                var conn = GetDatabase().Context.GetConnection();
                var organizationId = GetRepository().System.Organization.Insert(organization, conn);
                organizationId.IsEqualTo(0);
                uow.CommitAsync().Wait();
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
            return true;
        }

    }
}
