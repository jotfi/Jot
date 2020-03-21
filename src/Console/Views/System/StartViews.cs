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
                TextChanged = CheckConfirm
            });            
            AddToPanel(new Field("passwordInfo")
            {
                Secret = true,
                AutoSize = false,
                ShowTextField = false,
                LabelSize = (Dim.Fill(), 1)
            });
            if (ShowPanelDialog())
            {
                return true;
            }
            user.Password.CreatePassword = GetPanelText("password");
            user.Password.ConfirmPassword = GetPanelText("confirmPassword");
            return false;
        }

        void CheckPassword(string password)
        {
            var passwordInfo = GetStartViewModel().GetPasswordScoreInfo(password);
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
        }

        void CheckConfirm(string password)
        {
            var passwordsMatch = GetPanelText("password") == password;
            var passwordInfo = passwordsMatch ? "Passwords Match" : "Passwords do not match";
            Application.MainLoop.Invoke(() => SetPanelLabel("passwordInfo", passwordInfo));
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
