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
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.Classes;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.System
{
    public class SetupView : BaseView<SetupView, SystemService>, IConsoleView
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

        private readonly TerminalView Term;
        private readonly UserService Users;
        private readonly OrganizationService Organizations;

        public SetupView(IServiceProvider services) : base(services)
        {
            Term = services.GetRequiredService<TerminalView>();
            Users = services.GetRequiredService<UserService>();
            Organizations = services.GetRequiredService<OrganizationService>();
        }

        public bool Run()
        {
            try
            {
                if (MainService.IsSetup)
                {
                    return SetupConnection();
                }
                Term.Reset();
                Term.SetPanelTitle($"Welcome");
                Term.SetPanelPos(2, 2);
                Term.SetPanelSize(-4, -4);
                Term.AddToPanel(new Field(SetupInfo)
                {
                    ViewText = MainService.FirstTimeSetupText(),
                    ViewSize = (-1, 3),
                    ShowTextField = false
                });
                Term.AddToPanel(new Field(AdministratorExistsInfo)
                {
                    ViewText = MainService.AdministratorExists.ToCheckMark() + "Create Administrator User",
                    ShowTextField = false
                });
                Term.AddToPanel(new Field(OrganizationExistsInfo)
                {
                    ViewText = MainService.OrganizationExists.ToCheckMark() + "Create First Organization",
                    ShowTextField = false
                });
                var ok = GetOkButton("Setup");
                (ok.X, ok.Y) = (Pos.Center(), 10);
                ok.Clicked = () => BeginSetup();
                Term.AddToPanel(ok);
                Term.ShowPanel();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return false;
            }
        }

        public bool BeginSetup()
        {
            var admin = Users.FirstOrDefault(p => p.UserName == Constants.DefaultAdministratorName);
            if (admin == null)
            {
                admin = MainService.CreateAdminUser();
                if (!SetupAdministrator(admin))
                {
                    return false;
                }
                admin.UserName.IsEqualTo(Constants.DefaultAdministratorName);
            }
            if (!MainService.OrganizationExists)
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
                var id = MainService.SaveAdministrator(admin, out string error);
                ok = id > 0;
                if (ok)
                {
                    // admin = Services.System.User.GetUserById(id);
                }
                else
                {
                    ShowError(error);
                }
            }
            return ok;
        }

        bool SetupAdministratorDialog(User admin)
        {
            Term.Reset();
            Term.SetPanelTitle($"Welcome to {Constants.DefaultApplicationName}");
            Term.AddToPanel(new Field(AdministratorInfo)
            {
                ShowTextField = false,
                ViewText = MainService.CreateAdministratorText(),
                ViewSize = (-1, 4)
            });
            Term.AddToPanel(new Field(nameof(admin.CreatePassword), admin)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    ValidPassword = Users.GetPasswordValid(text);
                    var passwordsMatch = Term.GetPanelText(nameof(admin.ConfirmPassword)) == text;
                    var passwordInfo = Users.GetPasswordInfo(text);
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(AdministratorPasswordInfo, passwordInfo));
                    var infoColor = ValidPassword && passwordsMatch ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(AdministratorPasswordInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(nameof(admin.ConfirmPassword), admin)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    if (!ValidPassword || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var passwordsMatch = Term.GetPanelText(nameof(admin.CreatePassword)) == text;
                    var passwordInfo = passwordsMatch ? Users.GetPasswordInfo(text) : "Passwords do not match";
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(AdministratorPasswordInfo, passwordInfo));
                    var infoColor = passwordsMatch ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(AdministratorPasswordInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(AdministratorPasswordInfo)
            {
                ShowTextField = false
            });
            Term.AddToPanel(new Field(nameof(admin.Person.Contact.EmailAddress), admin.Person.Contact)
            {
                TextChanged = (text) =>
                {
                    ValidEmail = Users.GetPasswordValid(text);
                    var emailsMatch = Term.GetPanelText(nameof(admin.Person.Contact.ConfirmEmail)) == text;
                    var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(AdministratorEmailInfo, emailInfo));
                    var infoColor = ValidEmail && emailsMatch ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(AdministratorEmailInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(nameof(admin.Person.Contact.ConfirmEmail), admin.Person.Contact)
            {
                TextChanged = (text) =>
                {
                    if (!ValidEmail || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var emailsMatch = Term.GetPanelText(nameof(admin.Person.Contact.EmailAddress)) == text;
                    var emailInfo = emailsMatch ? "" : "Emails do not match";
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(AdministratorEmailInfo, emailInfo));
                    var infoColor = emailsMatch ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(AdministratorEmailInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(AdministratorEmailInfo)
            {
                ShowTextField = false
            });
            if (!Term.ShowPanelDialog())
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
                ok = Organizations.SaveOrganization(organization, out string error);
                if (!ok)
                {
                    ShowError(error);
                }
            }
            return ok;
        }

        bool SetupOrganizationDialog(Organization organization)
        {
            Term.Reset();
            Term.SetPanelTitle($"Setup your organization");
            Term.AddToPanel(new Field(OrganizationInfo)
            {
                ShowTextField = false,
                ViewText = MainService.FirstOrganizationText(),
                ViewSize = (-1, 4)
            });
            Term.AddToPanel(new Field(nameof(organization.Name), organization)
            {
                TextChanged = (text) =>
                {
                    var nameValid = !string.IsNullOrWhiteSpace(text);
                    var nameInfo = nameValid ? "" : "Name cannot be blank";
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(OrganizationNameInfo, nameInfo));
                    var infoColor = nameValid ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(OrganizationNameInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(AdministratorEmailInfo)
            {
                ShowTextField = false
            });
            if (!Term.ShowPanelDialog())
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
                if (Settings.IsClient)
                {
                    ok = MainService.CheckConnection(out string error);
                    if (!ok)
                    {
                        ShowError(error);
                    }
                }
            }
            if (ok)
            {
                Core.SaveSettings(Settings);
            }
            return ok;
        }

        bool SetupConnectionDialog()
        {
            Term.Reset();
            Term.SetPanelTitle($"Connect to {Constants.DefaultApplicationName} server");
            Term.AddToPanel(new Field(ServerInfo)
            {
                ViewText = MainService.ServerConnectionText(),
                ViewSize = (-1, 3),
                ShowTextField = false
            });
            var connectionTypes = new List<string>() { "Jot Server", "Local Connection" };
            var defaultConnection = Settings.IsClient ? 0 : 1;
            ConsoleUtils.ChangeSelection(connectionTypes, defaultConnection);
            var listView = new Terminal.Gui.ListView(connectionTypes)
            {
                SelectedItem = defaultConnection,
                TopItem = 0
            };
            Term.AddToPanel(new Field(ConnectionType, listView)
            {
                ViewSize = (-1, 3),
                ShowTextField = false,
                ListChanged = (index) =>
                {
                    ConsoleUtils.ChangeSelection(connectionTypes, index);
                    Settings.IsClient = index == 0;
                    var serverValid = HasServerInfo(out string serverInfo);
                    Term.MainLoop.Invoke(() => Term.SetPanelLabel(ServerUrlInfo, serverInfo));
                    var infoColor = serverValid ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(ServerUrlInfo, infoColor));
                }
            });
            Term.AddToPanel(new Field(nameof(Settings.ServerUrl), Settings)
            {
                Secret = false,
                TextChanged = (text) =>
                {
                    var serverValid = HasServerInfo(out string serverInfo);
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Term.MainLoop.Invoke(() => Term.SetPanelLabel(ServerUrlInfo, serverInfo));
                    }
                    else
                    {
                        serverValid = MainService.IsServerValid(text);
                        var urlInfo = serverValid ? "" : "Invalid server URL";
                        Term.MainLoop.Invoke(() => Term.SetPanelLabel(ServerUrlInfo, urlInfo));
                    }
                    var infoColor = serverValid ? Term.MenuColor : Term.ErrorColor;
                    Term.MainLoop.Invoke(() => Term.SetPanelColor(ServerUrlInfo, infoColor));
                }
            });
            var serverValid = HasServerInfo(out string serverInfo);
            Term.AddToPanel(new Field(ServerUrlInfo)
            {
                ViewText = serverInfo,
                ShowTextField = false,
                ViewColor = serverValid ? Term.MenuColor : Term.ErrorColor
            });
            return Term.ShowPanelDialog();
        }

        bool HasServerInfo(out string error)
        {
            if (!Settings.IsClient || !string.IsNullOrEmpty(Settings.ServerUrl))
            {
                error = string.Empty;
                return true;
            }
            error = $"Please enter the {Constants.DefaultApplicationName} server URL.";
            return false;
        }

    }
}
