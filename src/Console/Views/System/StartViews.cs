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
            var cancel = false;
            var complete = false;
            while (!cancel && !complete)
            {
                cancel = SetupAdministratorDialog(user);
                complete = IsAdministratorValid(user);
            }
            if (cancel)
            {
                return false;
            }
            return GetStartViewModel().SaveAdministrator();
        }

        bool SetupAdministratorDialog(User user)
        {
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
            if (ShowPanelDialog())
            {
                return true;
            }
            user.Password.CreatePassword = GetPanelText("password");
            user.Password.ConfirmPassword = GetPanelText("confirmPassword");
            user.Person.Email.EmailAddress = GetPanelText("email");
            user.Person.Email.ConfirmEmail = GetPanelText("confirmEmail");
            return false;
        }

        void CheckPassword(string password)
        {
            ValidPassword = GetStartViewModel().GetPasswordValid(password);
            var passwordInfo = GetStartViewModel().GetPasswordScoreInfo(password);
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
        }

        void CheckEmail(string email)
        {
            ValidEmail = GetStartViewModel().GetPasswordValid(email);
            var passwordInfo = GetStartViewModel().GetPasswordScoreInfo(email);
            Application.MainLoop.Invoke(() => SetPanelLabel("emailInfo", passwordInfo));
        }

        void CheckPasswordConfirm(string password)
        {
            if (!ValidPassword || string.IsNullOrEmpty(password))
            {
                return;
            }
            var passwordsMatch = GetPanelText("password") == password;
            var passwordInfo = passwordsMatch ? "Passwords Match" : "Passwords do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
            var infoColor = passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("passwordInfo", infoColor));
        }

        void CheckEmailConfirm(string password)
        {
            if (!ValidPassword || string.IsNullOrEmpty(password))
            {
                return;
            }
            var passwordsMatch = GetPanelText("password") == password;
            var passwordInfo = passwordsMatch ? "Passwords Match" : "Passwords do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("emailInfo", passwordInfo));
            var infoColor = passwordsMatch ? Colors.Menu : Colors.Error;
            Application.MainLoop.Invoke(() => SetPanelColor("emailInfo", infoColor));
        }

        bool IsAdministratorValid(User user)
        {
            return false;
        }

        public bool SetupOrganization()
        {
            throw new NotImplementedException();
        }
    }
}
