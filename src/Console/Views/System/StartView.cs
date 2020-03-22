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
    public class StartView : BaseView, IStartView
    {
        private bool ValidPassword;
        private bool ValidEmail;

        public StartView(ConsoleApplication app, BaseViewModel vm, LogOpts opts = null)
            : base(app, vm, opts)
        { 

        }

        public StartViewModel GetStartViewModel()
        {
            return (StartViewModel)GetViewModel();
        }

        public void ApplicationStart()
        {
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
                ok = GetStartViewModel().IsAdministratorValid(admin, out error);
                if (!ok)
                {
                    GetApp().ShowError(error);
                }
            }
            if (!ok)
            {
                return false;
            }
            return GetStartViewModel().SaveAdministrator(admin, out error);
        }

        bool SetupAdministratorDialog(User admin)
        {
            ClearPanel();
            SetPanelTitle($"Welcome to {Constants.DefaultApplicationName}");
            AddToPanel(new Field("infoText", GetStartViewModel().CreateAdministratorText())
            {
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field("password", "Administrator Password", admin.Password.CreatePassword)
            {
                Secret = true,
                TextChanged = CheckPassword
            });
            AddToPanel(new Field("confirmPassword", "Confirm Password", admin.Password.ConfirmPassword)
            {
                Secret = true,
                TextChanged = CheckPasswordConfirm
            });
            AddToPanel(new Field("passwordInfo")
            {
                AutoSize = false,
                ShowTextField = false
            });
            AddToPanel(new Field("email", "Administrator Email", admin.Person.Email.EmailAddress)
            {
                TextChanged = CheckEmail
            });
            AddToPanel(new Field("confirmEmail", "Confirm Email", admin.Person.Email.ConfirmEmail)
            {
                TextChanged = CheckEmailConfirm
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

        void CheckPassword(string password)
        {
            ValidPassword = GetViewModels().User.GetPasswordValid(password);
            var passwordsMatch = GetPanelText("confirmPassword") == password;
            var passwordInfo = GetViewModels().User.GetPasswordInfo(password);
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
            var infoColor = ValidPassword && passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("passwordInfo", infoColor));
        }

        void CheckPasswordConfirm(string password)
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

        void CheckEmail(string email)
        {
            ValidEmail = GetViewModels().User.GetPasswordValid(email);
            var emailsMatch = GetPanelText("confirmEmail") == email;
            var emailInfo = ValidEmail ? emailsMatch ? "" : "Confirm email address" : "Invalid email address";
            Application.MainLoop.Invoke(() => SetPanelLabel("emailInfo", emailInfo));
            var infoColor = ValidEmail && emailsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("emailInfo", infoColor));
        }

        void CheckEmailConfirm(string email)
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

        public bool SetupOrganization()
        {
            throw new NotImplementedException();
        }
    }
}
