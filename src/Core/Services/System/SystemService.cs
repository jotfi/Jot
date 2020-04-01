#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.
//
#endregion
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace jotfi.Jot.Core.Services.System
{
    public partial class SystemService : BaseService
    {
        private readonly UserService Users;
        private readonly ILogger Log;

        public SystemService(IOptions<AppSettings> settings,
            UserService users,
            ILogger<UserService> log) : base(settings)
        {
            Users = users;
            Log = log;
        }

        public bool CheckConnection(out string error)
        {
            error = $"Error connecting to {Settings.ServerUrl}: ";
            try
            {
                var response = Client.GetAsync("user").Result;
                if (response.IsSuccessStatusCode)
                {
                    error = "";
                    return true;
                }
                error += $"Status {response.StatusCode}";
                return false;
            }
            catch(Exception ex)
            {
                Log.LogError(ex, ex.Message);
                error += ex.Message;                
            }
            return false;
        }
        public bool IsSetup { get => AdministratorExists && OrganizationExists; }
        public bool AdministratorExists { get => false; }
        public bool OrganizationExists { get => false; }

        public User CreateAdminUser()
        {
            var admin = new User() 
            { 
                UserName = Constants.DefaultAdministratorName,
#if DEBUG
                CreatePassword = "admin1!",
                ConfirmPassword = "admin1!",
#endif
                Person = new Person()
                {
                    FirstName = "Admin",
                    LastName = "System",
                    Data = new ContactData()
                    {
#if DEBUG
                        EmailAddress = "admin@admin.com",
                        ConfirmEmail = "admin@admin.com"
#endif
                    }
                }
            };
            return admin;
        }

        public string FirstTimeSetupText()
        {
            return $@"
Setting up {Constants.DefaultApplicationName} for the first time. The following tasks must 
be completed before login dialog will be displayed.";
        }

        public string ServerConnectionText()
        {
            return $@"
If the {Constants.DefaultApplicationName} server is not available, selecting ""Local connection""
will attempt to use a direct connection to the database.";
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

        public bool IsServerValid(string url) => ValidUtils.IsUrlValid(url);

        public bool IsAdministratorValid(User user, out string error)
        {
            error = string.Empty;
            if (!Users.GetPasswordValid(user.CreatePassword))
            {
                error += "Invalid password. Password must not be too weak.\r\n";
                error += Users.GetPasswordInfo(user.CreatePassword);
                return false;
            }
            if (user.CreatePassword != user.ConfirmPassword)
            {
                error += "Invalid password. Confirm password does not match.";
                return false;
            }
            if (!Users.GetEmailValid(user.Person.Data.EmailAddress))
            {
                error += "Invalid email. Please check email address.";
                return false;
            }
            if (user.Person.Data.EmailAddress != user.Person.Data.ConfirmEmail)
            {
                error += "Invalid email. Confirm email does not match.";
                return false;
            }
            return true;
        }

        public long SaveAdministrator(User admin, out string error)
        {
            if (!IsAdministratorValid(admin, out error))
            {
                return 0;
            }
            return Users.CreateUser(admin);
        }

    }
}
