﻿using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views;
using jotfi.Jot.Core.Settings;
using System;
using System.Reflection;
using Terminal.Gui;

namespace jotfi.Jot.Console
{
    public class ConsoleApplication : Core.Application
    {        
        readonly Window MainWindow;
        readonly MenuBar MainMenu;
        readonly Toplevel StatusBar;

        public ConsoleApplication(AppSettings appSettings) : base(appSettings)
        {
            Views = new ConsoleViews(this);
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

        public override void Run()
        {            
            base.Run();
            AddStatus($"Version: {Assembly.GetEntryAssembly().GetName().Version}");
            try
            {
                if (!Services.System.Setup.IsSetup)
                {
                    Views.System.Setup.ShowSetup();
                }
                else if (!Views.System.Setup.SetupConnection())
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            Application.Top.Add(MainMenu);
            Application.Top.Add(MainWindow);
            Application.Top.Add(StatusBar);
            Application.Run();
        }

        public override void Quit()
        {
            base.Quit();
            var answer = MessageBox.Query(50, 7, $"Quit {Constants.DefaultApplicationName}", 
                $"Are you sure you want to quit {Constants.DefaultApplicationName}?", "Yes", "No");            
            Application.Top.Running = answer == 1;
        }

        public override void ShowError(string message)
        {
            base.ShowError(message);
            var width = 50;
            var height = message.Split("\r\n").Length + 6;
            var title = $"{Constants.DefaultApplicationName} Error";
            MessageBox.ErrorQuery(width, height, title, message, "Ok");
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
