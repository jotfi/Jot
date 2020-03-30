#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.
//
#endregion
using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core.Classes;
using jotfi.Jot.Core.Services.System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using Terminal.Gui;

namespace jotfi.Jot.Console.Classes
{
    public class ConsoleApp 
    {
        private readonly CoreApp App;
        private readonly TerminalView Term;
        private readonly SetupView Setup;
        private readonly ILogger Log;

        public ConsoleApp(CoreApp app, 
            TerminalView term, SetupView setup,
            ILogger<ConsoleApp> log)
        {
            App = app;
            Term = term;
            Setup = setup;
            Log = log;
        }

        public static void RegisterServices(IServiceCollection services)
        {
            var assembly = typeof(ConsoleApp).Assembly;
            var serviceTypes =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where typeof(BaseView).IsAssignableFrom(type)
                select type;
            foreach (var type in serviceTypes)
            {
                services.AddSingleton(type);
            }
            services.AddSingleton<ConsoleApp>();
        }

        public void Run()
        {   
            try
            {
                Term.AddStatus($"Version: {Assembly.GetEntryAssembly().GetName().Version}");
                App.Run();
                if (!Setup.Run())
                {
                    return;
                }
                Term.Run();
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }

    }
}
