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
using System.Threading.Tasks;

namespace jotfi.Jot.Console.Views.System
{
    public class SetupView : BaseView<SetupViewModel>, ISetupView
    {
        private bool ValidPassword;
        private bool ValidEmail;        
        const string ServerInfo = "ServerInfo";
        const string ConnectionType = "ConnectionType";
        const string ServerUrlInfo = "ServerUrlInfo";
        const string AdminInfo = "AdminInfo";
        const string AdminPasswordInfo = "AdminPasswordInfo";
        const string AdminEmailInfo = "AdminEmailInfo";
        const string OrganizationInfo = "OrganizationInfo";
        const string OrganizationNameInfo = "OrganizationNameInfo";

        public SetupView(Core.Application app, SetupViewModel viewmodel, LogOpts opts = null)
            : base(app, viewmodel, opts)
        {

        }

        public void ShowSetup()
        {
            ConsoleApp.AddMain(
            new Terminal.Gui.FrameView(new Terminal.Gui.Rect(3, 10, 25, 6), "Options"){
                new Terminal.Gui.CheckBox (1, 0, "Remember me"),
                new Terminal.Gui.RadioGroup (1, 2, new [] { "_Personal", "_Company" }),
            });
        }

        public bool SetupAdminOrganisation()
        {
            if (!AppSettings.IsClient)
            {
                if (!ViewModel.CheckDatabase(out string error))
                {
                    App.ShowError(error);
                    return false;
                }
            }
            if (!ViewModel.AdministratorExists)
            {
                var admin = ViewModel.CreateAdminUser();
                if (!SetupAdministrator(admin))
                {
                    return false;
                }
            }
            if (!ViewModel.OrganizationExists)
            {
                var organiation = new Organization();
                if (!SetupOrganization(organiation))
                {
                    return false;
                }
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
                    ok = ViewModel.CheckConnection(out string error);
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
            ClearPanel();
            SetPanelTitle($"Connect to {Constants.DefaultApplicationName} server");
            AddToPanel(new Field(ServerInfo)
            {
                ViewText = ViewModel.ServerConnectionText(),
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
                        serverValid = ViewModel.IsServerValid(text);
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
                ok = ViewModel.SaveAdministrator(admin, out string error);
                if (!ok)
                {
                    App.ShowError(error);
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
                ViewText = ViewModel.CreateAdministratorText(),
                ViewSize = (-1, 4)
            });
            AddToPanel(new Field(nameof(admin.Password.CreatePassword), admin.Password)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    ValidPassword = ViewModels.System.User.GetPasswordValid(text);
                    var passwordsMatch = GetPanelText(nameof(admin.Password.ConfirmPassword)) == text;
                    var passwordInfo = ViewModels.System.User.GetPasswordInfo(text);
                    MainLoop.Invoke(() => SetPanelLabel(AdminPasswordInfo, passwordInfo));
                    var infoColor = ValidPassword && passwordsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdminPasswordInfo, infoColor));
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
                    var passwordInfo = passwordsMatch ? ViewModels.System.User.GetPasswordInfo(text) : "Passwords do not match";
                    MainLoop.Invoke(() => SetPanelLabel(AdminPasswordInfo, passwordInfo));
                    var infoColor = passwordsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdminPasswordInfo, infoColor));
                }
            });
            AddToPanel(new Field(AdminPasswordInfo)
            {
                ShowTextField = false
            });
            AddToPanel(new Field(nameof(admin.Person.Email.EmailAddress), admin.Person.Email)
            {
                TextChanged = (text) =>
                {
                    ValidEmail = ViewModels.System.User.GetPasswordValid(text);
                    var emailsMatch = GetPanelText(nameof(admin.Person.Email.ConfirmEmail)) == text;
                    var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
                    MainLoop.Invoke(() => SetPanelLabel(AdminEmailInfo, emailInfo));
                    var infoColor = ValidEmail && emailsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdminEmailInfo, infoColor));
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
                    MainLoop.Invoke(() => SetPanelLabel(AdminEmailInfo, emailInfo));
                    var infoColor = emailsMatch ? MenuColor : ErrorColor;
                    MainLoop.Invoke(() => SetPanelColor(AdminEmailInfo, infoColor));
                }
            });
            AddToPanel(new Field(AdminEmailInfo)
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
                ok = ViewModels.System.Organization.SaveOrganization(organization, out string error);
                if (!ok)
                {
                    App.ShowError(error);
                }
            }
            return ok;
        }

        bool SetupOrganizationDialog(Organization organization)
        {
            ClearPanel();
            SetPanelTitle($"Setup your organization");
            AddToPanel(new Field(OrganizationInfo)
            {
                ShowTextField = false,
                ViewText = ViewModel.FirstOrganizationText(),
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
            AddToPanel(new Field(AdminEmailInfo)
            {
                ShowTextField = false
            });
            if (!ShowPanelDialog())
            {
                return false;
            }
            return true;
        }

    }
}
