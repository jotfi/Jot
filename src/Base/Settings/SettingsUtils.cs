// Copyright 2020 John Cottrell
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

using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace jotfi.Jot.Base.Settings
{
    public class SettingsUtils
    {
        static Logger Log { get; } = LogManager.GetLogger(typeof(SettingsUtils).FullName);

        public static T GetSettings<T>()
        {
            T appSettings = default;
            var settingsFile = GetSettingsFile();
            if (File.Exists(settingsFile))
            {
                var settingsString = File.ReadAllText(settingsFile);
                try
                {
                    appSettings = (T)JsonConvert.DeserializeObject(settingsString, typeof(T));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            return appSettings;
        }

        public static void SaveSettings(BaseSettings settings) => 
            File.WriteAllText(GetSettingsFile(), JsonConvert.SerializeObject(settings));

        static string GetSettingsFile()
        {
            var appPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            return Path.Combine(appPath, "appsettings.json");
        }
    }
}
