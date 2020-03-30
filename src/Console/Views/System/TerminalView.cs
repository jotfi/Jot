#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion
using jotfi.Jot.Console.Views.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jotfi.Jot.Console.Classes;
using Mono.Terminal;
using Terminal.Gui;
using Microsoft.Extensions.Logging;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Base.System;
using System.Reflection;

namespace jotfi.Jot.Console.Views.System
{
    public class TerminalView : BaseView
    {
        private readonly ILogger Log;
        private readonly Window MainWindow;
        private readonly MenuBar MainMenu;
        private readonly Toplevel StatusBar;
        private List<Panel> Panels { get; } = new List<Panel>();

        public TerminalView()
        {
            Application.Init();
            MainMenu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", Quit)
                }),
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", "", Quit)
                })
            });
            MainWindow = new Window(Constants.DefaultApplicationName)
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - Dim.Sized(1)
            };
            StatusBar = new Toplevel()
            {
                X = 0,
                Y = Pos.Bottom(Application.Top) - 1,
                Width = Dim.Fill(),
                Height = Dim.Sized(1),
                ColorScheme = Colors.Dialog
            };
        }

        public override bool Run()
        {
            Application.Top.Add(MainMenu);
            Application.Top.Add(MainWindow);
            Application.Top.Add(StatusBar);
            Application.Run();
            return true;
        }

        public MainLoop MainLoop => Application.MainLoop;
        public Dim DimFill => Dim.Fill();
        public ColorScheme MenuColor => Colors.Menu;
        public ColorScheme ErrorColor => Colors.Error;
        public void Reset() => Panels.Clear();
        public void AddToTop(View view) => Application.Top.Add(view);
        public void AddToPanel(Field field, 
            string panelId = "main") => GetPanel(panelId).Fields.Add(field);
        public void AddToPanel(View view,
            string panelId = "main") => GetPanel(panelId).Views.Add(view);
        public void SetPanelTitle(string title, 
            string panelId = "main") => GetPanel(panelId).Title = title;
        public void SetPanelPos(int x, int y,
            string panelId = "main") => GetPanel(panelId).Pos = (x, y);
        public void SetPanelSize(int width, int height,
            string panelId = "main") => GetPanel(panelId).Size = (width, height);
        public string GetPanelText(string id, 
            string panelId = "main") => GetPanel(panelId).GetText(id);
        public void SetPanelText(string id, string text, 
            string panelId = "main") => GetPanel(panelId).SetText(id, text);
        public void SetPanelLabel(string id, string text, 
            string panelId = "main") => GetPanel(panelId).SetLabel(id, text);
        public void SetPanelColor(string id, Terminal.Gui.ColorScheme color, 
            string panelId = "main") => GetPanel(panelId).SetColor(id, color);

        public bool ShowPanelDialog(string id = "", bool showCancel = true,
            string panelId = "main", string okCaption = "Ok", string cancelCaption = "Cancel")
        {
            return GetPanel(panelId).ShowDialog(id, showCancel, (okCaption, cancelCaption));
        }

        public void ShowPanel(string id = "", string panelId = "main")
        {
            AddMain(GetPanel(panelId).GetFrameView(id));
        }

        public Panel GetPanel(string panelId = "main")
        {
            if (!Panels.Any(p => p.Id == panelId))
            {
                var newPanel = new Panel(panelId);
                Panels.Add(newPanel);
                return newPanel;
            }
            return Panels.Find(p => p.Id == panelId);
        }

        public void AddMain(params View[] views)
        {
            MainWindow.Add(views);
        }

        public void AddMain(View view, bool clear = false)
        {
            if (clear)
            {
                MainWindow.RemoveAll();
            }
            MainWindow.Add(view);
        }

        public void AddStatus(string message)
        {
            AddStatus(new Label(3, 0, message));
        }

        public void AddStatus(View view, bool clear = true)
        {
            if (clear)
            {
                StatusBar.RemoveAll();
            }
            StatusBar.Add(view);
        }

        public void UpdateMainMenu()
        {
            Application.MainLoop.Invoke(LoadMainMenu);
        }

        void LoadMainMenu()
        {
            MainMenu.Clear();
        }
    }
}
