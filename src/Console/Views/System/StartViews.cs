using jotfi.Jot.Base.System;
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
            AddToPanel(new Field("info", GetStartViewModel().CreateAdministratorText())
            {
                AutoAlign = false,
                AutoSize = false,
                ShowTextField = false,
                LabelPos = (1, 1),
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field("password", "Administrator Password: ", user.Password.CreatePassword)
            {
                TextChanged = PasswordChanged
            });
            AddToPanel(new Field("confpass", "Confirm Password: ", user.Password.ConfirmPassword));            
            AddToPanel(new Field("pwdstr")
            {
                AutoSize = false,
                ShowTextField = false,
                LabelSize = (Dim.Fill(), 1)
            });
            if (ShowPanelDialog())
            {
                return true;
            }
            user.Password.CreatePassword = GetPanelText("password");
            user.Password.ConfirmPassword = GetPanelText("confpass");
            return false;
        }

        void PasswordChanged(TextBox tb)
        {
            Application.MainLoop.Invoke(() => SetPanelLabel("pwdstr", $"Hello World {tb.Text}"));
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
