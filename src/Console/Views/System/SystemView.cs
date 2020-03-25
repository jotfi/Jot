using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.System;
using jotfi.Jot.Model.System;
using System.Collections.Generic;

namespace jotfi.Jot.Console.Views.System
{
    public class SystemView : BaseView<SystemViewModel>, ISystemView
    {
        private bool ValidPassword;
        private bool ValidEmail;        
        const string ServerInfo = "ServerInfo";
        const string UrlInfo = "UrlInfo";
        const string AdminInfo = "AdminInfo";
        const string PasswordInfo = "PasswordInfo";
        const string EmailInfo = "EmailInfo";
        const string ConnectionType = "ConnectionType";

        public SystemView(ConsoleApplication app, SystemViewModel viewmodel, LogOpts opts = null)
            : base(app, viewmodel, opts)
        {

        }

        public void ApplicationStart()
        {
            if (!SetupConnection())
            {
                GetAppSettings().IsClient = false;
                if (!GetViewModel().CheckDatabase(out string error))
                {
                    GetApp().ShowError(error);
                }
            }            
            if (!GetViewModel().CheckAdministrator())
            {
                var admin = GetViewModel().CreateAdminUser();
                if (!SetupAdministrator(admin))
                {
                    return;
                }                
            }
            if (!GetViewModel().CheckOrganization())
            {
                var organiation = new Organization();
                if (!SetupOrganization(organiation))
                {
                    return;
                }                
            }
            Terminal.Gui.Application.Run();
        }

        public void ApplicationEnd()
        {

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
                string error;
                (ok, error) = GetViewModel().CheckConnectionAsync().Result;
                if (!ok)
                {
                    GetApp().ShowError(error);
                }
            }
            if (ok)
            {
                GetApp().SaveSettings();
            }
            return ok;
        }

        bool SetupConnectionDialog()
        {
            var settings = GetAppSettings();
            ClearPanel();
            SetPanelTitle($"Connect to {Constants.DefaultApplicationName} server");
            AddToPanel(new Field(ServerInfo)
            {
                ViewText = GetViewModel().ServerConnectionText(),
                ViewSize = (-1, 3),                
                ShowTextField = false
            });
            var connectionTypes = new List<string>() { "Jot Server", "Local Connection" };
            ConsoleUtils.ChangeSelection(connectionTypes);
            var listView = new Terminal.Gui.ListView(connectionTypes);
            AddToPanel(new Field(ConnectionType, listView)
            {
                ViewSize = (-1, 2),
                ShowTextField = false,
                ListChanged = (index) =>
                {
                    ConsoleUtils.ChangeSelection(connectionTypes, index);
                    settings.IsClient = index == 0;
                }
            });
            var serverInfo = $"Please enter the {Constants.DefaultApplicationName} server URL.";
            AddToPanel(new Field(nameof(settings.ServerUrl), settings)
            {
                Secret = false,
                TextChanged = (text) =>
                {
                    var serverValid = false;
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        GetMainLoop().Invoke(() => SetPanelLabel(UrlInfo, serverInfo));
                    }
                    else
                    {
                        serverValid = GetViewModel().IsServerValid(text);
                        var urlInfo = serverValid ? "" : "Invalid server URL";
                        GetMainLoop().Invoke(() => SetPanelLabel(UrlInfo, urlInfo));
                    }
                    var infoColor = serverValid ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(UrlInfo, infoColor));
                }
            });
            var serverText = string.IsNullOrEmpty(settings.ServerUrl) ? serverInfo : "";
            AddToPanel(new Field(UrlInfo)
            {
                ViewText = serverText,
                ShowTextField = false,
                ViewColor = GetErrorColor()
            });
            if (!ShowPanelDialog(nameof(settings.ServerUrl)))
            {
                return false;
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
                ok = GetViewModel().SaveAdministrator(admin, out string error);
                if (!ok)
                {
                    GetApp().ShowError(error);
                }
            }            
            return ok;
        }

        bool SetupAdministratorDialog(User admin)
        {
            ClearPanel();
            SetPanelTitle($"Welcome to {Constants.DefaultApplicationName}");
            AddToPanel(new Field(AdminInfo)
            {
                ShowTextField = false,
                ViewText = GetViewModel().CreateAdministratorText(),
                ViewSize = (-1, 4)
            });
            AddToPanel(new Field(nameof(admin.Password.CreatePassword), admin.Password)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    ValidPassword = GetViewModels().User.GetPasswordValid(text);
                    var passwordsMatch = GetPanelText(nameof(admin.Password.ConfirmPassword)) == text;
                    var passwordInfo = GetViewModels().User.GetPasswordInfo(text);
                    GetMainLoop().Invoke(() => SetPanelLabel(PasswordInfo, passwordInfo));
                    var infoColor = ValidPassword && passwordsMatch ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(PasswordInfo, infoColor));
                }

            });
            AddToPanel(new Field(nameof(admin.Password.ConfirmPassword), admin.Password)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    if (!ValidPassword || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var passwordsMatch = GetPanelText(nameof(admin.Password.CreatePassword)) == text;
                    var passwordInfo = passwordsMatch ? GetViewModels().User.GetPasswordInfo(text) : "Passwords do not match";
                    GetMainLoop().Invoke(() => SetPanelLabel(PasswordInfo, passwordInfo));
                    var infoColor = passwordsMatch ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(PasswordInfo, infoColor));
                }
            });
            AddToPanel(new Field(PasswordInfo)
            {
                ShowTextField = false
            });
            AddToPanel(new Field(nameof(admin.Person.Email.EmailAddress), admin.Person.Email)
            {
                TextChanged = (text) =>
                {
                    ValidEmail = GetViewModels().User.GetPasswordValid(text);
                    var emailsMatch = GetPanelText(nameof(admin.Person.Email.ConfirmEmail)) == text;
                    var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
                    GetMainLoop().Invoke(() => SetPanelLabel(EmailInfo, emailInfo));
                    var infoColor = ValidEmail && emailsMatch ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(EmailInfo, infoColor));
                }
            });
            AddToPanel(new Field(nameof(admin.Person.Email.ConfirmEmail), admin.Person.Email)
            {
                TextChanged = (text) =>
                {
                    if (!ValidEmail || string.IsNullOrEmpty(text))
                    {
                        return;
                    }
                    var emailsMatch = GetPanelText(nameof(admin.Person.Email.EmailAddress)) == text;
                    var emailInfo = emailsMatch ? "" : "Emails do not match";
                    GetMainLoop().Invoke(() => SetPanelLabel(EmailInfo, emailInfo));
                    var infoColor = emailsMatch ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(EmailInfo, infoColor));
                }
            });
            AddToPanel(new Field(EmailInfo)
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
                ok = GetViewModel().SaveOrganization(organization, out string error);
                if (!ok)
                {
                    GetApp().ShowError(error);
                }
            }
            return ok;
        }

        bool SetupOrganizationDialog(Organization organization)
        {
            ClearPanel();
            SetPanelTitle($"Setup your organization");
            if (!ShowPanelDialog())
            {
                return false;
            }
            return true;
        }

    }
}
