using Nista.Jottre.Console.Windows.Base;
using Nista.Jottre.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Windows.System
{
    public class LoginWindow : BaseWindow, ILoginView
    {
        public LoginWindow(WindowController win) : base(win, "Jottre Login")
        {
            
        }

        public void ShowLogin()
        {
            App.Top.Add(Window);
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", () => { if (App.Quit()) App.Top.Running = false; })
                })
            });
            App.Top.Add(menu);

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
            Window.Add(
                // The ones with my favorite layout system
                login, password, loginText, passText,
                        new Button(3, 14, "Ok", true)
                        {
                            Clicked = () =>
                        {
                            MessageBox.Query(50, 5, "Login", $"{loginText.Text} {passText.Text}");
                        }
                        },
                        new Button(13, 14, "Quit") { Clicked = () => { if (App.Quit()) App.Top.Running = false; } },
                        new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));            
        }
    }
}
