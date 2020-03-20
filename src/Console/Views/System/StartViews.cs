using johncocom.Jot.Base.System;
using johncocom.Jot.Console.Views.Base;
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
            bool cancel = false;
            var saveButton = new Button(3, 14, "Save")
            {
                Clicked = () => Application.RequestStop()
            };
            var cancelButton = new Button(10, 14, "Cancel")
            {
                Clicked = () => { Application.RequestStop(); cancel = true; }
            };
            var dialog = new Dialog($"Welcome to {Constants.DefaultApplicationName}", 80, 24, saveButton, cancelButton);
            var infoLabel = new Label(GetStartViewModel().CreateAdministratorText())
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill(),
                Height = 4
            };
            var passLabel = new Label("Administrator Password: ")
            {
                X = Pos.Left(infoLabel),
                Y = Pos.Bottom(infoLabel) + 1,
            };
            var passText = new TextField("")
            {
                Secret = true,
                X = Pos.Right(passLabel),
                Y = Pos.Top(passLabel),
                Width = Dim.Fill()
            };
            var repeatLabel = new Label("Repeat Password: ")
            {
                X = Pos.Left(passLabel),
                Y = Pos.Bottom(passLabel) + 1,
                Width = Dim.Width(passLabel)
            };
            var repeatText = new TextField("")
            {
                Secret = true,
                X = Pos.Right(repeatLabel),
                Y = Pos.Top(repeatLabel),
                Width = Dim.Fill()
            };
            dialog.Add(infoLabel, passLabel, passText, repeatLabel, repeatText);
            dialog.SetFocus(passText);
            Application.Run(dialog);
            return cancel;
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
