using Nista.Jottre.Console.Windows.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Windows.System
{
    public class LoginWindow : BaseWindow
    {
        public LoginWindow() : base("Jottre Login")
        {
            
        }

        public void Run(Toplevel top)
        {
            // Creates the top-level window to show
            var win = new Window("Jottre Login")
            {
                X = 0,
                Y = 1, // Leave one row for the toplevel menu

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);

            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => { if (Program.Quit()) top.Running = false; })
            })
            });
            top.Add(menu);

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
            win.Add(
                // The ones with my favorite layout system
                login, password, loginText, passText,
                        new Button(3, 14, "Ok", true)
                        {
                            Clicked = () =>
                        {
                            MessageBox.Query(50, 5, "Login", $"{loginText.Text} {passText.Text}");
                        }
                        },
                        new Button(13, 14, "Quit") { Clicked = () => { if (Program.Quit()) top.Running = false; } },
                        new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));            
        }
    }
}
