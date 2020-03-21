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
    public class StartViews : BaseView, IStartViews
    {
        private bool ValidPassword;
        private bool ValidEmail;

        public StartViews(ConsoleApplication app, BaseViewModel vm, LogOpts opts = null)
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

        public bool SetupAdministrator()
        {
            var user = new User();
            var ok = false;
            while (!ok)
            {
                ok = SetupAdministratorDialog(user);
                if (!ok)
                {
                    break;
                }
                ok = IsAdministratorValid(user);
            }
            if (!ok)
            {
                return false;
            }
            return GetStartViewModel().SaveAdministrator();
        }

        bool SetupAdministratorDialog(User user)
        {
            ClearPanel();
            SetPanelTitle($"Welcome to {Constants.DefaultApplicationName}");
            AddToPanel(new Field("infoText", GetStartViewModel().CreateAdministratorText())
            {
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelPos = (1, 1),
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field("password", "Administrator Password", user.Password.CreatePassword)
            {
                Secret = true,
                TextChanged = CheckPassword
            });
            AddToPanel(new Field("confirmPassword", "Confirm Password", user.Password.ConfirmPassword)
            {
                Secret = true,
                TextChanged = CheckPasswordConfirm
            });
            AddToPanel(new Field("passwordInfo")
            {
                AutoSize = false,
                ShowTextField = false,
                ColorScheme = Colors.Error
            });
            AddToPanel(new Field("email", "Administrator Email", user.Person.Email.EmailAddress)
            {
                TextChanged = CheckEmail
            });
            AddToPanel(new Field("confirmEmail", "Confirm Email", user.Person.Email.ConfirmEmail)
            {
                TextChanged = CheckEmailConfirm
            });
            AddToPanel(new Field("emailInfo")
            {
                AutoSize = false,
                ShowTextField = false,
                ColorScheme = Colors.Error
            });
            if (!ShowPanelDialog())
            {
                return false;
            }
            user.Password.CreatePassword = GetPanelText("password");
            user.Password.ConfirmPassword = GetPanelText("confirmPassword");
            user.Person.Email.EmailAddress = GetPanelText("email");
            user.Person.Email.ConfirmEmail = GetPanelText("confirmEmail");
            return true;
        }

        void CheckPassword(string password)
        {
            ValidPassword = GetStartViewModel().GetPasswordValid(password);
            var passwordsMatch = GetPanelText("confirmPassword") == password;
            var passwordInfo = GetStartViewModel().GetPasswordScoreInfo(password);
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
            var passwordInfo = passwordsMatch ? GetStartViewModel().GetPasswordScoreInfo(password) : "Passwords do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
            var infoColor = passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("passwordInfo", infoColor));
        }

        void CheckEmail(string email)
        {
            ValidEmail = GetStartViewModel().GetPasswordValid(email);
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

        bool IsAdministratorValid(User user)
        {
            if (!GetStartViewModel().GetPasswordValid(user.Password.CreatePassword))
            {
                var passwordInfo = "Invalid password. Password must not be too weak.\r\n";
                passwordInfo += GetStartViewModel().GetPasswordScoreInfo(user.Password.CreatePassword);
                GetApp().ShowError(passwordInfo);
                return false;
            }
            if (user.Password.CreatePassword != user.Password.ConfirmPassword)
            {
                GetApp().ShowError("Invalid password. Confirm password does not match.");
                return false;
            }
            if (!GetStartViewModel().GetEmailValid(user.Person.Email.EmailAddress))
            {
                GetApp().ShowError("Invalid email. Please check email address.");
                return false;
            }
            if (user.Person.Email.EmailAddress != user.Person.Email.ConfirmEmail)
            {
                GetApp().ShowError("Invalid email. Confirm email does not match.");
                return false;
            }
            return true;
        }

        public bool SetupOrganization()
        {
            throw new NotImplementedException();
        }
    }
}
