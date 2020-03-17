using Mono.Terminal;
using Nista.Jottre.Console.Windows.System;
using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace Nista.Jottre.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			Application.Init();
			new LoginWindow().Run(Application.Top);
			Application.Run();
		}

		public static bool Quit()
		{
			var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
			return n == 0;
		}
	}
}
