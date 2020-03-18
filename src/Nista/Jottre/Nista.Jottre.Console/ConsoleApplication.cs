
namespace Nista.Jottre.Console
{
    public class ConsoleApplication : Core.Application
    {
        public readonly Terminal.Gui.Toplevel Top;
        public readonly ConsoleViewController Win;

        public ConsoleApplication() : base(true)
        {
            Terminal.Gui.Application.Init();
            Top = Terminal.Gui.Application.Top;
            Win = new ConsoleViewController(this);
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

        public override void MessageBox(string message)
        {
            base.MessageBox(message);
            Terminal.Gui.MessageBox.ErrorQuery(50, 7, "Jottre Error", message);
        }
    }
}
