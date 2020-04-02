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
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using jotfi.Jot.Core.Services.Base;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using jotfi.Jot.Database.Classes;

namespace jotfi.Jot.Core.Classes
{
    public class CoreApp
    {
        public readonly string EnvironmentName;
        public readonly AppSettings Settings;
        private readonly ILogger Log;

        public CoreApp(IHostEnvironment env, IOptions<AppSettings> settings, ILogger<CoreApp> log)
        {
            EnvironmentName = env.EnvironmentName;
            Settings = settings.Value;
            Log = log;
        }

        public static void RegisterServices(IServiceCollection services)
        {
            var assembly = typeof(CoreApp).Assembly;
            var serviceTypes =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where typeof(IService).IsAssignableFrom(type)
                select type;
            foreach (var type in serviceTypes)
            {
                services.AddSingleton(type);
            }
            DbManager.RegisterServices(services);
            services.AddSingleton<CoreApp>();
        }

        public void Run()
        {
            var manager = new DbManager(Settings.Database);
            manager.Run();
        }

        /// <summary>
        /// https://stackoverflow.com/questions/41653688/asp-net-core-appsettings-json-update-in-code
        /// </summary>
        public void SaveSettings<T>(T value, string sectionPathKey = Constants.DefaultApplicationName)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, $"appsettings.{EnvironmentName}.json");
                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(AppContext.BaseDirectory, $"appsettings.json");
                }
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                SetValueRecursively(sectionPathKey, jsonObj, value);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Error writing app settings | {0}", ex.Message);
            }
        }

        private void SetValueRecursively<T>(string sectionPathKey, dynamic jsonObj, T value)
        {
            // Split at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);
            var currentSection = remainingSections[0];
            if (remainingSections.Length > 1)
            {
                // Continue moving down the tree
                var nextSection = remainingSections[1];
                SetValueRecursively(nextSection, jsonObj[currentSection], value);
            }
            else
            {
                // End of the tree, set the value
                jsonObj[currentSection] = value;
            }
        }
    }
}
