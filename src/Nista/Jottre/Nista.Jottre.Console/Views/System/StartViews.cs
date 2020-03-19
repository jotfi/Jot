using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.Base;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Core.ViewModels.System;
using Nista.Jottre.Core.Views.System;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.System
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
            var user = new User()
            {
                Password = new Model.Base.Password(),
                Person = new Person()
                {
                    Email = new Model.Base.Email(user.Person)
                }
            };
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
            var dialog = new Dialog("Welcome to Jottre", 80, 24, saveButton, cancelButton);
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
