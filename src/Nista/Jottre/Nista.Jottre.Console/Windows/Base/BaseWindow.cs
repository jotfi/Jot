using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console.Windows.Base
{
    public abstract class BaseWindow
    {
        protected readonly WindowController Win;
        protected readonly GuiApplication App;
        protected readonly Terminal.Gui.Window Window;
        
        public BaseWindow(WindowController win, string title)
        {
            Win = win;
            App = win.App;            
            Window = new Terminal.Gui.Window(title)
            {
                X = 0,
                Y = 1,
                Width = Terminal.Gui.Dim.Fill(),
                Height = Terminal.Gui.Dim.Fill()
            };
        }
    }
}
