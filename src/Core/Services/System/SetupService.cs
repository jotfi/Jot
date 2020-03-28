﻿// Copyright 2020 John Cottrell
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

using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.System
{
    public partial class SetupService : BaseService
    {
        public SetupService(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public bool CheckConnection(out string error)
        {
            error = $"Error connecting to {AppSettings.ServerUrl}: ";
            try
            {
                var client = App.Client;
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                client.BaseAddress = new Uri(AppSettings.ServerUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(mediaType);
                //using var cts = new CancellationTokenSource(new TimeSpan(0, 0, 5));
                //var response = await client.GetAsync("user", cts.Token).ConfigureAwait(false);
                client.Timeout = new TimeSpan(0, 0, 5);
                var response = client.GetAsync("user").Result;
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
                Log(ex);
                error += ex.Message;                
            }
            return false;
        }
        public bool IsSetup { get => AdministratorExists && OrganizationExists; }
        //public bool CheckDatabase(out string error) => Database.CheckTables(GetTableNames(), out error);
        public bool AdministratorExists { get => Repository.System.User.Exists(); }
        public bool OrganizationExists { get => Repository.System.Organization.Exists(); }

        public User CreateAdminUser()
        {
            var admin = new User() { UserName = Constants.DefaultAdministratorName };
            admin.Person.FirstName = "Admin";
            admin.Person.LastName = "System";
#if DEBUG
            var password = "admin1!";
            admin.CreatePassword = password;
            admin.ConfirmPassword = password;
            var email = "admin@admin.com";
            admin.Person.ContactDetails.EmailAddress = email;
            admin.Person.ContactDetails.ConfirmEmail = email;
#endif
            return admin;
        }

        public List<TableName> GetTableNames(object whereConditions = null)
        {
            whereConditions ??= new { Type = "table" };
            return Repository.System.TableName.GetList(whereConditions).ToList();
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
            if (!Services.System.User.GetPasswordValid(user.CreatePassword))
            {
                error += "Invalid password. Password must not be too weak.\r\n";
                error += Services.System.User.GetPasswordInfo(user.CreatePassword);
                return false;
            }
            if (user.CreatePassword != user.ConfirmPassword)
            {
                error += "Invalid password. Confirm password does not match.";
                return false;
            }
            if (!Services.System.User.GetEmailValid(user.Person.ContactDetails.EmailAddress))
            {
                error += "Invalid email. Please check email address.";
                return false;
            }
            if (user.Person.ContactDetails.EmailAddress != user.Person.ContactDetails.ConfirmEmail)
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
            return Services.System.User.CreateUser(admin);
        }

    }
}