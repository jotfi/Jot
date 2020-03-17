using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Windows.Base
{
    public abstract class BaseWindow
    {
        protected readonly Window Window;
        public BaseWindow(string title)
        {
            Window = new Window(title)
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
        }
    }
}
