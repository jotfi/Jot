using Mono.Terminal;
using Nista.Jottre.Console.Windows.System;
using System;
using System.Collections.Generic;

namespace Nista.Jottre.Console
{
	public class Program
	{

		static void Main(string[] args)
		{
			var app = new GuiApplication();
			app.Run();
		}

		public static bool Quit()
		{
			var n = Terminal.Gui.MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
			return n == 0;
		}
	}
}
