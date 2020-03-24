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
    public class SystemView : BaseView, ISystemView
    {
        private bool ValidPassword;
        private bool ValidEmail;

        public SystemView(ConsoleApplication app, SystemViewModel vm, LogOpts opts = null)
            : base(app, vm, opts)
        {

        }
        public SystemViewModel GetSystemViewModel() => (SystemViewModel)GetViewModel();

        public void ApplicationStart()
        {
            GetSystemViewModel().CheckDatabase();
            if (!GetSystemViewModel().CheckAdministrator())
            {
                var admin = GetSystemViewModel().CreateAdminUser();
                if (!SetupAdministrator(admin, out string error))
                {
                    GetApp().ShowError(error);
                    return;
                }                
            }
            if (!GetSystemViewModel().CheckOrganization())
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
                ok = GetSystemViewModel().SaveAdministrator(admin, out error);
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
            AddToPanel(new Field("infoText", GetSystemViewModel().CreateAdministratorText())
            {
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field("password", "Administrator Password", admin.Password.CreatePassword)
            {
                Secret = true,
                TextChanged = CheckAdminPassword
            });
            AddToPanel(new Field("confirmPassword", "Confirm Password", admin.Password.ConfirmPassword)
            {
                Secret = true,
                TextChanged = CheckAdminPasswordConfirm
            });
            AddToPanel(new Field("passwordInfo")
            {
                AutoSize = false,
                ShowTextField = false
            });
            AddToPanel(new Field("email", "Administrator Email", admin.Person.Email.EmailAddress)
            {
                TextChanged = CheckAdminEmail
            });
            AddToPanel(new Field("confirmEmail", "Confirm Email", admin.Person.Email.ConfirmEmail)
            {
                TextChanged = CheckAdminEmailConfirm
            });
            AddToPanel(new Field("emailInfo")
            {
                AutoSize = false,
                ShowTextField = false
            });
            if (!ShowPanelDialog())
            {
                return false;
            }
            admin.Password.CreatePassword = GetPanelText("password");
            admin.Password.ConfirmPassword = GetPanelText("confirmPassword");
            admin.Person.Email.EmailAddress = GetPanelText("email");
            admin.Person.Email.ConfirmEmail = GetPanelText("confirmEmail");
            return true;
        }

        void CheckAdminPassword(string password)
        {
            ValidPassword = GetViewModels().User.GetPasswordValid(password);
            var passwordsMatch = GetPanelText("confirmPassword") == password;
            var passwordInfo = GetViewModels().User.GetPasswordInfo(password);
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
            var infoColor = ValidPassword && passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("passwordInfo", infoColor));
        }

        void CheckAdminPasswordConfirm(string password)
        {
            if (!ValidPassword || string.IsNullOrEmpty(password))
            {
                return;
            }
            var passwordsMatch = GetPanelText("password") == password;
            var passwordInfo = passwordsMatch ? GetViewModels().User.GetPasswordInfo(password) : "Passwords do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
            var infoColor = passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("passwordInfo", infoColor));
        }

        void CheckAdminEmail(string email)
        {
            ValidEmail = GetViewModels().User.GetPasswordValid(email);
            var emailsMatch = GetPanelText("confirmEmail") == email;
            var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
            Application.MainLoop.Invoke(() => SetPanelLabel("emailInfo", emailInfo));
            var infoColor = ValidEmail && emailsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("emailInfo", infoColor));
        }

        void CheckAdminEmailConfirm(string email)
        {
            if (!ValidEmail || string.IsNullOrEmpty(email))
            {
                return;
            }
            var emailsMatch = GetPanelText("email") == email;
            var emailInfo = emailsMatch ? "" : "Emails do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("emailInfo", emailInfo));
            var infoColor = emailsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("emailInfo", infoColor));
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
                ok = GetSystemViewModel().SaveOrganization(organization, out error);
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
