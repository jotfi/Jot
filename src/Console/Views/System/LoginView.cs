﻿// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.System
{
    public class LoginView : BaseView<LoginService>, ILoginView
    {

        public LoginView(Core.Application app, LogOpts opts = null)
            : base(app, app.Services.System.Login, opts)
        {
            
        }

        public bool ShowLogin()
        {
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

            ConsoleApp.AddStatus("Press F9 (on Unix, ESC+9 is an alias) to activate the menubar");

            // Add some controls, 
            ConsoleApp.AddMain(
                // The ones with my favorite layout system
                login, password, loginText, passText,
                        new Button(3, 14, "Ok", true)
                        {
                            Clicked = () =>
                        {
                            MessageBox.Query(50, 5, "Login", $"{loginText.Text} {passText.Text}");
                        }
                        });

            return true;
        }


        public List<MenuBarItem> GetMainMenuItems()
        {
            var mainMenuItems = new List<MenuBarItem>();

            return mainMenuItems;
        }

        public void AddMainMenu()
        {
            throw new NotImplementedException();
        }

    }
}
