// Copyright 2020 John Cottrell
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
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Core.Views.System;
using jotfi.Jot.Model.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jotfi.Jot.Console.Views.System
{
    public class SetupView : BaseView<SetupService>, ISetupView
    {
        private bool ValidPassword;
        private bool ValidEmail;
        const string SetupInfo = "SetupInfo";
        const string AdministratorExistsInfo = "AdministratorExistsInfo";
        const string OrganizationExistsInfo = "OrganizationExistsInfo";
        const string ServerInfo = "ServerInfo";
        const string ConnectionType = "ConnectionType";
        const string ServerUrlInfo = "ServerUrlInfo";
        const string AdministratorInfo = "AdministratorInfo";
        const string AdministratorPasswordInfo = "AdministratorPasswordInfo";
        const string AdministratorEmailInfo = "AdministratorEmailInfo";
        const string OrganizationInfo = "OrganizationInfo";
        const string OrganizationNameInfo = "OrganizationNameInfo";

        public SetupView(Core.Application app, LogOpts opts = null)
            : base(app, app.Services.System.Setup, opts)
        {

        }

        public void ShowSetup()
        {
            Reset();
            SetPanelTitle($"Welcome");
            SetPanelPos(2, 2);
            SetPanelSize(-4, -4);
            AddToPanel(new Field(SetupInfo)
            {
                ViewText = Service.FirstTimeSetupText(),
                ViewSize = (-1, 3),
                ShowTextField = false
            });
            AddToPanel(new Field(AdministratorExistsInfo)
            {
                ViewText = Service.AdministratorExists.ToCheckMark() + "Create Administrator User",
                ShowTextField = false
            });
            AddToPanel(new Field(OrganizationExistsInfo)
            {
                ViewText = Service.OrganizationExists.ToCheckMark() + "Create First Organization",
                ShowTextField = false
            });
            var ok = GetOkButton("Setup");
            (ok.X, ok.Y) = (Terminal.Gui.Pos.Center(), 10);
            ok.Clicked = () => BeginSetup();
            AddToPanel(ok);
            ShowPanel();
        }

        public bool BeginSetup()
        {
            //if (!AppSettings.IsClient)
            //{
            //    if (!Service.CheckDatabase(out string error))
            //    {
            //        App.ShowError(error);
            //        return false;
            //    }
            //}
            //var admin = Services.System.User.GetUserByName(Constants.DefaultAdministratorName);
            User admin = null;
            if (admin == null)
            {
                admin = Service.CreateAdminUser();
                if (!SetupAdministrator(admin))
                {
                    return false;
                }
                admin.UserName.IsEqualTo(Constants.DefaultAdministratorName);
            }
            if (!Service.OrganizationExists)
            {
                var organiation = new Organization();
                if (!SetupOrganization(organiation))
                {
                    return false;
                }
            }
            return true;
        }        

        public bool SetupAdministrator(User admin)
        {
            var ok = false;
            while (!ok)
            {
                ok = SetupAdministratorDialog(admin);
                if (!ok)
                {
                    break;
                }
                var id = Service.SaveAdministrator(admin, out string error);
                ok = id > 0;
                if (ok)
                {
                    // admin = Services.System.User.GetUserById(id);
                }
                else
                {
                    App.ShowError(error);
                }
            }            
            return ok;
        }

        bool SetupAdministratorDialog(User admin)
        {
            Reset();
            SetPanelTitle($"Welcome to {Constants.DefaultApplicationName}");
            AddToPanel(new Field(AdministratorInfo)
            {
                ShowTextField = false,
                ViewText = Service.CreateAdministratorText(),
                ViewSize = (-1, 4)
            });
            AddToPanel(new Field(nameof(admin.CreatePassword), admin)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    ValidPassword = Services.System.User.GetPasswordValid(text);
                    var passwordsMatch = GetPanelText(nameof(admin.ConfirmPassword)) == text;
                    var passwordInfo = Services.System.User.GetPasswordInfo(text);
                    MainLoop.Invoke(() => SetPanelLabel(AdministratorPasswordInfo, passwordInfo));
                    var infoColor = ValidPassword && passwordsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdministratorPasswordInfo, infoColor));
                }
            });
            AddToPanel(new Field(nameof(admin.ConfirmPassword), admin)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    if (!ValidPassword || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var passwordsMatch = GetPanelText(nameof(admin.CreatePassword)) == text;
                    var passwordInfo = passwordsMatch ? Services.System.User.GetPasswordInfo(text) : "Passwords do not match";
                    MainLoop.Invoke(() => SetPanelLabel(AdministratorPasswordInfo, passwordInfo));
                    var infoColor = passwordsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdministratorPasswordInfo, infoColor));
                }
            });
            AddToPanel(new Field(AdministratorPasswordInfo)
            {
                ShowTextField = false
            });
            AddToPanel(new Field(nameof(admin.Person.ContactDetails.EmailAddress), admin.Person.ContactDetails)
            {
                TextChanged = (text) =>
                {
                    ValidEmail = Services.System.User.GetPasswordValid(text);
                    var emailsMatch = GetPanelText(nameof(admin.Person.ContactDetails.ConfirmEmail)) == text;
                    var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
                    MainLoop.Invoke(() => SetPanelLabel(AdministratorEmailInfo, emailInfo));
                    var infoColor = ValidEmail && emailsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdministratorEmailInfo, infoColor));
                }
            });
            AddToPanel(new Field(nameof(admin.Person.ContactDetails.ConfirmEmail), admin.Person.ContactDetails)
            {
                TextChanged = (text) =>
                {
                    if (!ValidEmail || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var emailsMatch = GetPanelText(nameof(admin.Person.ContactDetails.EmailAddress)) == text;
                    var emailInfo = emailsMatch ? "" : "Emails do not match";
                    MainLoop.Invoke(() => SetPanelLabel(AdministratorEmailInfo, emailInfo));
                    var infoColor = emailsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdministratorEmailInfo, infoColor));
                }
            });
            AddToPanel(new Field(AdministratorEmailInfo)
            {
                ShowTextField = false
            });
            if (!ShowPanelDialog())
            {
                return false;
            }
            return true;
        }

        public bool SetupOrganization(Organization organization)
        {
            var ok = false;
            while (!ok)
            {
                ok = SetupOrganizationDialog(organization);
                if (!ok)
                {
                    break;
                }
                ok = Services.System.Organization.SaveOrganization(organization, out string error);
                if (!ok)
                {
                    App.ShowError(error);
                }
            }
            return ok;
        }

        bool SetupOrganizationDialog(Organization organization)
        {
            Reset();
            SetPanelTitle($"Setup your organization");
            AddToPanel(new Field(OrganizationInfo)
            {
                ShowTextField = false,
                ViewText = Service.FirstOrganizationText(),
                ViewSize = (-1, 4)
            });
            AddToPanel(new Field(nameof(organization.Name), organization)
            {
                TextChanged = (text) =>
                {
                    var nameValid = !string.IsNullOrWhiteSpace(text);
                    var nameInfo = nameValid ? "" : "Name cannot be blank";
                    MainLoop.Invoke(() => SetPanelLabel(OrganizationNameInfo, nameInfo));
                    var infoColor = nameValid ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(OrganizationNameInfo, infoColor));
                }
            });
            AddToPanel(new Field(AdministratorEmailInfo)
            {
                ShowTextField = false
            });
            if (!ShowPanelDialog())
            {
                return false;
            }
            return true;
        }

        public bool SetupConnection()
        {
            var ok = false;
            while (!ok)
            {
                ok = SetupConnectionDialog();
                if (!ok)
                {
                    break;
                }
                if (AppSettings.IsClient)
                {
                    ok = Service.CheckConnection(out string error);
                    if (!ok)
                    {
                        App.ShowError(error);
                    }
                }
            }
            if (ok)
            {
                App.SaveSettings();
            }
            return ok;
        }

        bool SetupConnectionDialog()
        {
            var settings = AppSettings;
            Reset();
            SetPanelTitle($"Connect to {Constants.DefaultApplicationName} server");
            AddToPanel(new Field(ServerInfo)
            {
                ViewText = Service.ServerConnectionText(),
                ViewSize = (-1, 3),
                ShowTextField = false
            });
            var connectionTypes = new List<string>() { "Jot Server", "Local Connection" };
            var defaultConnection = settings.IsClient ? 0 : 1;
            ConsoleUtils.ChangeSelection(connectionTypes, defaultConnection);
            var listView = new Terminal.Gui.ListView(connectionTypes)
            {
                SelectedItem = defaultConnection,
                TopItem = 0
            };
            AddToPanel(new Field(ConnectionType, listView)
            {
                ViewSize = (-1, 3),
                ShowTextField = false,
                ListChanged = (index) =>
                {
                    ConsoleUtils.ChangeSelection(connectionTypes, index);
                    settings.IsClient = index == 0;
                    var serverValid = HasServerInfo(out string serverInfo);
                    MainLoop.Invoke(() => SetPanelLabel(ServerUrlInfo, serverInfo));
                    var infoColor = serverValid ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(ServerUrlInfo, infoColor));
                }
            });
            AddToPanel(new Field(nameof(settings.ServerUrl), settings)
            {
                Secret = false,
                TextChanged = (text) =>
                {
                    var serverValid = HasServerInfo(out string serverInfo);
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        MainLoop.Invoke(() => SetPanelLabel(ServerUrlInfo, serverInfo));
                    }
                    else
                    {
                        serverValid = Service.IsServerValid(text);
                        var urlInfo = serverValid ? "" : "Invalid server URL";
                        MainLoop.Invoke(() => SetPanelLabel(ServerUrlInfo, urlInfo));
                    }
                    var infoColor = serverValid ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(ServerUrlInfo, infoColor));
                }
            });
            var serverValid = HasServerInfo(out string serverInfo);
            AddToPanel(new Field(ServerUrlInfo)
            {
                ViewText = serverInfo,
                ShowTextField = false,
                ViewColor = serverValid ? MenuColor : ErrorColor
            });
            return ShowPanelDialog();
        }

        bool HasServerInfo(out string error)
        {
            var settings = App.AppSettings;
            if (!settings.IsClient || !string.IsNullOrEmpty(settings.ServerUrl))
            {
                error = string.Empty;
                return true;
            }
            error = $"Please enter the {Constants.DefaultApplicationName} server URL.";
            return false;
        }

    }
}
