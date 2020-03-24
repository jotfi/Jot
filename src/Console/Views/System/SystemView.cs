using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.System;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.System
{
    public class SystemView : BaseView<SystemViewModel>, ISystemView
    {
        private bool ValidPassword;
        private bool ValidEmail;
        const string HeaderInfo = "HeaderInfo";
        const string PasswordInfo = "PasswordInfo";
        const string EmailInfo = "EmailInfo";

        public SystemView(ConsoleApplication app, SystemViewModel viewmodel, LogOpts opts = null)
            : base(app, viewmodel, opts)
        {

        }

        public void ApplicationStart()
        {
            GetViewModel().CheckDatabase();
            if (!GetViewModel().CheckAdministrator())
            {
                var admin = GetViewModel().CreateAdminUser();
                if (!SetupAdministrator(admin, out string error))
                {
                    GetApp().ShowError(error);
                    return;
                }                
            }
            if (!GetViewModel().CheckOrganization())
            {
                var organiation = new Organization();
                if (!SetupOrganization(organiation, out string error))
                {
                    GetApp().ShowError(error);
                    return;
                }                
            }
            Application.Run();
        }

        public bool SetupAdministrator(User admin, out string error)
        {
            error = string.Empty;
            var ok = false;
            while (!ok)
            {
                ok = SetupAdministratorDialog(admin);
                if (!ok)
                {
                    break;
                }
                ok = GetViewModel().SaveAdministrator(admin, out error);
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
            AddToPanel(new Field(HeaderInfo)
            {
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelText = GetViewModel().CreateAdministratorText(),
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field(nameof(admin.Password.CreatePassword), admin.Password)
            {
                Secret = true,
                TextChanged = (text) =>
                {
                    ValidPassword = GetViewModels().User.GetPasswordValid(text);
                    var passwordsMatch = GetPanelText(nameof(admin.Password.ConfirmPassword)) == text;
                    var passwordInfo = GetViewModels().User.GetPasswordInfo(text);
                    Application.MainLoop.Invoke(() => SetPanelLabel(PasswordInfo, passwordInfo));
                    var infoColor = ValidPassword && passwordsMatch ? Colors.Menu : Colors.Error;
                    Application.MainLoop.Invoke(() => SetPanelColor(PasswordInfo, infoColor));
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
                    Application.MainLoop.Invoke(() => SetPanelLabel(PasswordInfo, passwordInfo));
                    var infoColor = passwordsMatch ? Colors.Menu : Colors.Error;
                    Application.MainLoop.Invoke(() => SetPanelColor(PasswordInfo, infoColor));
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
                    Application.MainLoop.Invoke(() => SetPanelLabel(EmailInfo, emailInfo));
                    var infoColor = ValidEmail && emailsMatch ? Colors.Menu : Colors.Error;
                    Application.MainLoop.Invoke(() => SetPanelColor(EmailInfo, infoColor));
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
                    Application.MainLoop.Invoke(() => SetPanelLabel(EmailInfo, emailInfo));
                    var infoColor = emailsMatch ? Colors.Menu : Colors.Error;
                    Application.MainLoop.Invoke(() => SetPanelColor(EmailInfo, infoColor));
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


        public bool SetupOrganization(Organization organization, out string error)
        {
            error = string.Empty;
            var ok = false;
            while (!ok)
            {
                ok = SetupOrganizationDialog(organization);
                if (!ok)
                {
                    break;
                }
                ok = GetViewModel().SaveOrganization(organization, out error);
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
