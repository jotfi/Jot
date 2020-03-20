using johncocom.Jot.Base.System;
using johncocom.Jot.Console.Views.Base;
using johncocom.Jot.Console.Views.Controls;
using johncocom.Jot.Core.ViewModels.Base;
using johncocom.Jot.Core.ViewModels.System;
using johncocom.Jot.Core.Views.System;
using johncocom.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.System
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
                ShowTextField = false,
                LabelPos = (1, 1),
                LabelSize = (Dim.Fill(), 4)
            });
            AddToPanel(new Field("password", "Administrator Password: "));
            AddToPanel(new Field("confpass", "Confirm Password: "));            
            return ShowPanelDialog();
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
