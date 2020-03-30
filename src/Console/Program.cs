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
using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Core.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace jotfi.Jot.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var appSettingsSection = Constants.DefaultApplicationName;
                    services.Configure<AppSettings>(options =>
                    {
                        hostContext.Configuration.GetSection(appSettingsSection).Bind(options);
                    });
                    CoreApp.RegisterServices(services);
                    ConsoleApp.RegisterServices(services);
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                    var seriLogConfig = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .CreateLogger();
                    configLogging.AddSerilog(seriLogConfig);
                })
                .UseConsoleLifetime()
                .Build();

                using (var serviceScope = host.Services.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var app = services.GetRequiredService<ConsoleApp>();
                    app.Run();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error Occured {ex.Message}");
            }

        }
    }
}
