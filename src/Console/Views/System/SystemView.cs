using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.System;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Console.Views.System
{
    public class SystemView : BaseView<SystemViewModel>, ISystemView
    {
        private bool ValidPassword;
        private bool ValidEmail;
        const string ServerInfo = "ServerInfo";
        const string AdminInfo = "AdminInfo";
        const string PasswordInfo = "PasswordInfo";
        const string EmailInfo = "EmailInfo";

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
                ok = GetViewModel().CheckConnection();
                if (ok)
                {
                    GetApp().SaveSettings();
                }
            }
            return ok;
        }

        bool SetupConnectionDialog()
        {
            ClearPanel();
            SetPanelTitle($"Connect to {Constants.DefaultApplicationName} server");
            var settings = GetAppSettings();
            AddToPanel(new Field(nameof(settings.ServerUrl), settings)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    var validUrl = GetViewModels().User.GetPasswordValid(text);
                    var serverInfo = validUrl ? "" : "Invalid server URL";
                    GetMainLoop().Invoke(() => SetPanelLabel(ServerInfo, serverInfo));
                    var infoColor = validUrl ? GetMenuColor() : GetErrorColor();
                    GetMainLoop().Invoke(() => SetPanelColor(ServerInfo, infoColor));
                }
            });
            AddToPanel(new Field(ServerInfo)
            {
                AutoSize = false,
                ShowTextField = false
            });
            if (!ShowPanelDialog())
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
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelText = GetViewModel().CreateAdministratorText(),
                LabelSize = (GetFill(), 4)
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
                AutoSize = false,
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
                AutoSize = false,
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
