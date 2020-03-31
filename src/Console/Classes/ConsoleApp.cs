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
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.System;
using jotfi.Jot.Core.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace jotfi.Jot.Console.Classes
{
    public class ConsoleApp
    {
        private readonly CoreApp Core;
        private readonly ILogger Log;
        private readonly AppSettings Settings;
        private readonly TerminalView Term;
        private readonly SetupView Setup;

        public ConsoleApp(IServiceProvider services) 
        {
            Core = services.GetRequiredService<CoreApp>();
            Log = services.GetRequiredService<ILogger<ConsoleApp>>();
            Settings = services.GetRequiredService<IOptions<AppSettings>>().Value;
            Term = services.GetRequiredService<TerminalView>();
            Setup = services.GetRequiredService<SetupView>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            var assembly = typeof(ConsoleApp).Assembly;
            var serviceTypes =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where typeof(IConsoleView).IsAssignableFrom(type)
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
                Core.Run();
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
