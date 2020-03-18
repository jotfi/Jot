using Nista.Jottre.Base.System;
using Nista.Jottre.Console.Views.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.System
{
    public class LoginViews : BaseView, ILoginViews
    {
        public LoginViews(ConsoleApplication app, LogOpts opts = null) : base(app, opts)
        {
            
        }

        public void ShowLogin()
        {
            var window = GetWindow("Jottre Login");
            AddToTop(window);
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", () => Quit())
                })
            });
            AddToTop(menu);

            var login = new Label("Login: ") { X = 3, Y = 2 };
            var password = new Label("Password: ")
            {
                X = Pos.Left(login),
                Y = Pos.Top(login) + 2
            };
            var loginText = new TextField("")
            {
                X = Pos.Right(password),
                Y = Pos.Top(login),
                Width = 40
            };
            var passText = new TextField("")
            {
                Secret = true,
                X = Pos.Left(loginText),
                Y = Pos.Top(password),
                Width = Dim.Width(loginText)
            };

            // Add some controls, 
            window.Add(
                // The ones with my favorite layout system
                login, password, loginText, passText,
                        new Button(3, 14, "Ok", true)
                        {
                            Clicked = () =>
                        {
                            MessageBox.Query(50, 5, "Login", $"{loginText.Text} {passText.Text}");
                        }
                        },
                        new Button(13, 14, "Quit") { Clicked = () => Quit() },
                        new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));            
        }
    }
}
