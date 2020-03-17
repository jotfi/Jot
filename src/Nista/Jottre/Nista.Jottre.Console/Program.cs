using Mono.Terminal;
using Nista.Jottre.Console.Views.System;
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
	}
}
