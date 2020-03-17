using Nista.Jottre.Console.Windows.Base;
using Nista.Jottre.Console.Windows.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console
{
    public class GuiApplication : Core.Application
    {
        public readonly Terminal.Gui.Toplevel Top;
        public readonly WindowController Win;

        public GuiApplication()
        {
            Terminal.Gui.Application.Init();
            Top = Terminal.Gui.Application.Top;
            Win = new WindowController(this);
        }

        public override void Run()
        {
            Win.Login.Run();
            Terminal.Gui.Application.Run();
        }
    }
}
