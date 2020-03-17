using Nista.Jottre.Console.Views.Base;
using Nista.Jottre.Console.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Console
{
    public class GuiApplication : Core.Application
    {
        public readonly Terminal.Gui.Toplevel Top;
        public readonly WindowController Win;

        public GuiApplication() : base(true)
        {
            Terminal.Gui.Application.Init();
            Top = Terminal.Gui.Application.Top;
            Win = new WindowController(this);
            Init(Win);
        }

        public override void Run()
        {
            base.Run();            
            Terminal.Gui.Application.Run();
        }

        public override bool Quit()
        {
            base.Quit();
            var n = Terminal.Gui.MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
            return n == 0;
        }
    }
}
