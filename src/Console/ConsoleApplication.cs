using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views;
using Terminal.Gui;

namespace jotfi.Jot.Console
{
    public class ConsoleApplication : Core.Application
    {
        readonly Toplevel StatusBar;
        readonly Window MainWindow;

        public ConsoleApplication() : base(true)
        {
            Views = new ConsoleViewController(this);
            Application.Init();
            Application.Top.Add(MainWindow = new Window(Constants.DefaultApplicationName)
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - Dim.Sized(1)
            });
            Application.Top.Add(StatusBar = new Toplevel()
            {
                X = 0,
                Y = Pos.Bottom(Application.Top) - 1,
                Width = Dim.Fill(),
                Height = Dim.Sized(1)
            });
            Init();
        }

        public override bool Quit()
        {
            base.Quit();
            var n = MessageBox.Query(50, 7, $"Quit {Constants.DefaultApplicationName}", 
                $"Are you sure you want to quit {Constants.DefaultApplicationName}?", "Yes", "No");
            return n == 0;
        }

        public override void ShowError(string message)
        {
            base.ShowError(message);
            MessageBox.ErrorQuery(50, 7, $"{Constants.DefaultApplicationName} Error", message, "Ok");
        }

        public void AddMain(params View[] views)
        {
            MainWindow.RemoveAll();
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
    }
}
